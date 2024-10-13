using System;

// Interface untuk kemampuan
interface IKemampuan
{
    void Gunakan(Robot target);
    int GetCooldown(); 
}

// Abstract class Robot
abstract class Robot
{
    public string Nama { get; set; }
    public int Energi { get; set; }
    public int Armor { get; set; }
    public int Serangan { get; set; }

    public Robot(string nama, int energi, int armor, int serangan)
    {
        this.Nama = nama;
        this.Energi = energi;
        this.Armor = armor;
        this.Serangan = serangan;
    }

    public abstract void Serang(Robot target);

    public abstract void GunakanKemampuan(IKemampuan kemampuan, Robot target);

    public void CetakInformasi()
    {
        Console.WriteLine($"Nama: {Nama}");
        Console.WriteLine($"Energi (Asap): {Energi}");
        Console.WriteLine($"Armor: {Armor}");
        Console.WriteLine($"Serangan: {Serangan}");
        Console.WriteLine();
    }
}

// Kelas untuk robot biasa bernama Nicobot
class Nicobot : Robot
{
    public Nicobot(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan) { }

    public override void Serang(Robot target)
    {
        Console.WriteLine($"{Nama} menghembuskan asap ke {target.Nama}");
        int damage = Serangan - target.Armor;
        if (damage > 0)
        {
            target.Energi -= damage;
            Console.WriteLine($"{target.Nama} terkena asap dan kehilangan {damage} energi.");
        }
        else
        {
            Console.WriteLine($"{target.Nama} berhasil menghindari asap karena armornya.");
        }
    }

    public override void GunakanKemampuan(IKemampuan kemampuan, Robot target)
    {
        kemampuan.Gunakan(target);
    }
}

// Kelas untuk bos robot bernama Smokezilla
class Smokezilla : Robot
{
    public Smokezilla(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan) { }

    public override void Serang(Robot target)
    {
        Console.WriteLine($"{Nama} melepaskan asap pekat ke {target.Nama} dengan serangan yang sangat mematikan!");
        int damage = Serangan - target.Armor;
        if (damage > 0)
        {
            target.Energi -= damage;
            Console.WriteLine($"{target.Nama} kehilangan {damage} energi karena serangan asap.");
        }
        else
        {
            Console.WriteLine($"{target.Nama} tidak terpengaruh oleh asap.");
        }
    }

    public void Diserang(Robot penyerang)
    {
        Console.WriteLine($"{Nama} diserang oleh {penyerang.Nama}");
        int damage = penyerang.Serangan - Armor;
        if (damage > 0)
        {
            Energi -= damage;
            Console.WriteLine($"{Nama} menerima {damage} kerusakan.");
        }
        else
        {
            Console.WriteLine($"{Nama} berhasil menghindari kerusakan karena armor.");
        }
    }

    public void Mati()
    {
        if (Energi <= 0)
        {
            Console.WriteLine($"{Nama} telah dikalahkan!");
        }
    }

    public override void GunakanKemampuan(IKemampuan kemampuan, Robot target)
    {
        kemampuan.Gunakan(target);
    }
}

// Kemampuan: Regenerasi Asap untuk memulihkan energi
class RegenerasiAsap : IKemampuan
{
    private int healAmount = 30;
    private int cooldown = 3;

    public void Gunakan(Robot target)
    {
        Console.WriteLine($"{target.Nama} menghirup udara segar dan memulihkan {healAmount} energi.");
        target.Energi += healAmount;
    }

    public int GetCooldown() => cooldown;
}

// Kemampuan: Serangan Nikotin
class SeranganNikotin : IKemampuan
{
    private int damage = 20;
    private int cooldown = 2;

    public void Gunakan(Robot target)
    {
        Console.WriteLine($"{target.Nama} terkena Serangan Nikotin dan mengurangi {damage} energi.");
        target.Energi -= damage;
    }

    public int GetCooldown() => cooldown;
}

// Kemampuan: tembakan Rokok Plasma 
class RokokPlasma : IKemampuan
{
    private int damage = 40;
    private int cooldown = 5;

    public void Gunakan(Robot target)
    {
        Console.WriteLine($"{target.Nama} terkena tembakan Rokok Plasma dan mengurangi {damage} energi.");
        target.Energi -= damage;
    }

    public int GetCooldown() => cooldown;
}

// Kemampuan: meningkatkan pertahanan Tembok Asap, disini tembok asap bermakna seperti armor
class TembokAsap : IKemampuan
{
    private int armorBoost = 20;
    private int cooldown = 4;

    public void Gunakan(Robot target)
    {
        Console.WriteLine($"{target.Nama} meningkatkan pertahanannya dengan Tembok Asap.");
        target.Armor += armorBoost;
    }

    public int GetCooldown() => cooldown;
}

class Program
{
    static void Main(string[] args)
    {
        Nicobot robot1 = new Nicobot("Nicobot Mild", 100, 10, 20);
        Nicobot robot2 = new Nicobot("Nicobot Menthol", 100, 15, 18);
        Smokezilla boss = new Smokezilla("Smokezilla Max", 300, 30, 40);

        // Memberikan informasi awal
        robot1.CetakInformasi();
        robot2.CetakInformasi();
        boss.CetakInformasi();

        // Bertarung antar robot
        robot1.Serang(boss);
        boss.Diserang(robot1);
        boss.Diserang(robot2);

        RegenerasiAsap regenerasi = new RegenerasiAsap();
        robot1.GunakanKemampuan(regenerasi, robot1);

        SeranganNikotin nikotinShock = new SeranganNikotin();
        robot2.GunakanKemampuan(nikotinShock, boss);

        boss.Mati();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Produkt
{
    private string nazwa;
    private decimal cenaNetto;
    private string kategoriaVAT;

    public static readonly Dictionary<string, decimal> StawkiVAT = new Dictionary<string, decimal>
    {
        { "A", 0.23m },
        { "B", 0.08m },
        { "C", 0.05m },
        { "D", 0.0m }
    };

    public static readonly HashSet<string> KrajePochodzenia = new HashSet<string>
    {
        "Polska",
        "Niemcy",
        "Francja",
        "Hiszpania"
    };

    public string Nazwa
    {
        get => nazwa;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Nazwa nie może być pusta.");
            }
            nazwa = value;
        }
    }

    public decimal CenaNetto
    {
        get => cenaNetto;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Cena netto nie może być ujemna.");
            }
            cenaNetto = value;
        }
    }

    public string KategoriaVAT
    {
        get => kategoriaVAT;
        set
        {
            if (!StawkiVAT.ContainsKey(value))
            {
                throw new ArgumentException("Nieprawidłowa kategoria VAT.");
            }
            kategoriaVAT = value;
        }
    }

    public virtual decimal CenaBrutto
    {
        get => CenaNetto * (1 + StawkiVAT[KategoriaVAT]);
    }

    public virtual string KrajPochodzenia { get; set; }

    protected Produkt(string nazwa, decimal cenaNetto, string kategoriaVAT, string krajPochodzenia)
    {
        Nazwa = nazwa;
        CenaNetto = cenaNetto;
        KategoriaVAT = kategoriaVAT;
        KrajPochodzenia = krajPochodzenia;
    }
}

public abstract class ProduktSpożywczy : Produkt
{
    private decimal kalorie;

    public static readonly HashSet<string> KategorieVATSpożywcze = new HashSet<string>
    {
        "A",
        "B"
    };

    public static readonly HashSet<string> AlergenyPrzewidywane = new HashSet<string>
    {
        "Orzechy",
        "Gluten",
        "Laktoza",
        "Soja"
    };

    public decimal Kalorie
    {
        get => kalorie;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Kalorie nie mogą być ujemne.");
            }
            kalorie = value;
        }
    }

    public HashSet<string> Alergeny { get; set; }

    protected ProduktSpożywczy(string nazwa, decimal cenaNetto, string kategoriaVAT, string krajPochodzenia, decimal kalorie)
        : base(nazwa, cenaNetto, kategoriaVAT, krajPochodzenia)
    {
        if (!KategorieVATSpożywcze.Contains(kategoriaVAT))
        {
            throw new ArgumentException("Nieprawidłowa kategoria VAT dla produktu spożywczego.");
        }

        Kalorie = kalorie;
        Alergeny = new HashSet<string>();
    }
}

public class ProduktSpożywczyNaWagę : ProduktSpożywczy
{
    public ProduktSpożywczyNaWagę(string nazwa, decimal cenaNetto, string kategoriaVAT, string krajPochodzenia, decimal kalorie)
        : base(nazwa, cenaNetto, kategoriaVAT, krajPochodzenia, kalorie)
    {
    }
}

public class ProduktSpożywczyPaczka : ProduktSpożywczy
{
    private decimal waga;

    public decimal Waga
    {
        get => waga;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Waga nie może być ujemna.");
            }
            waga = value;
        }
    }

    public ProduktSpożywczyPaczka(string nazwa, decimal cenaNetto, string kategoriaVAT, string krajPochodzenia, decimal kalorie, decimal waga)
        : base(nazwa, cenaNetto, kategoriaVAT, krajPochodzenia, kalorie)
    {
        Waga = waga;
    }
}

public class ProduktSpożywczyNapój : ProduktSpożywczyPaczka
{
    private uint objętość;

    public uint Objętość
    {
        get => objętość;
        set
        {
            if (value == 0)
            {
                throw new ArgumentException("Objętość nie może być zerowa.");
            }
            objętość = value;
        }
    }

    public ProduktSpożywczyNapój(string nazwa, decimal cenaNetto, string kategoriaVAT, string krajPochodzenia, decimal kalorie, decimal waga, uint objętość)
        : base(nazwa, cenaNetto, kategoriaVAT, krajPochodzenia, kalorie, waga)
    {
        Objętość = objętość;
    }
}

public class Wielopak
{
    private Produkt produkt;
    private ushort ilość;
    private decimal cenaNetto;

    public Produkt Produkt
    {
        get => produkt;
        set => produkt = value ?? throw new ArgumentException("Produkt nie może być null.");
    }

    public ushort Ilość
    {
        get => ilość;
        set
        {
            if (value == 0)
            {
                throw new ArgumentException("Ilość nie może być zerowa.");
            }
            ilość = value;
        }
    }

    public decimal CenaNetto
    {
        get => cenaNetto;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Cena netto nie może być ujemna.");
            }
            cenaNetto = value;
        }
    }

    public decimal CenaBrutto
    {
        get => CenaNetto * (1 + Produkt.StawkiVAT[Produkt.KategoriaVAT]);
    }

    public string KategoriaVAT
    {
        get => Produkt.KategoriaVAT;
    }

    public string KrajPochodzenia
    {
        get => Produkt.KrajPochodzenia;
    }

    public Wielopak(Produkt produkt, ushort ilość, decimal cenaNetto)
    {
        Produkt = produkt;
        Ilość = ilość;
        CenaNetto = cenaNetto;
    }
}

// Przykład użycia
public class Program
{
    public static void Main()
    {
        try
        {
            ProduktSpożywczyPaczka paczka = new ProduktSpożywczyPaczka("Czekolada", 5.0m, "A", "Polska", 500, 0.1m);
            ProduktSpożywczyNapój napój = new ProduktSpożywczyNapój("Sok", 3.0m, "B", "Polska", 40, 1.0m, 250);

            Console.WriteLine($"Produkt: {paczka.Nazwa}, Cena netto: {paczka.CenaNetto}, Cena brutto: {paczka.CenaBrutto}");
            Console.WriteLine($"Produkt: {napój.Nazwa}, Cena netto: {napój.CenaNetto}, Cena brutto: {napój.CenaBrutto}");

            Wielopak wielopak = new Wielopak(paczka, 10, 45.0m);
            Console.WriteLine($"Wielopak: Produkt: {wielopak.Produkt.Nazwa}, Ilość: {wielopak.Ilość}, Cena netto: {wielopak.CenaNetto}, Cena brutto: {wielopak.CenaBrutto}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

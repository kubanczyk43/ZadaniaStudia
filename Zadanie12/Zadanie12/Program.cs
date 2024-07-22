using System;
using System.Collections.Generic;

public class Prostokąt
{
    // Prywatne pola
    private double _bokA;
    private double _bokB;

    // Publiczne właściwości
    public double BokA
    {
        get => _bokA;
        set
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || value < 0)
            {
                throw new ArgumentException("BokA musi być skończoną, nieujemną liczbą");
            }
            _bokA = value;
        }
    }

    public double BokB
    {
        get => _bokB;
        set
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || value < 0)
            {
                throw new ArgumentException("BokB musi być skończoną, nieujemną liczbą");
            }
            _bokB = value;
        }
    }

    // Statyczny słownik
    public static readonly Dictionary<char, double> wysokośćArkusza0 = new Dictionary<char, double>
    {
        ['A'] = 1189,
        ['B'] = 1414,
        ['C'] = 1297
    };

    // Statyczne pole dla pierwiastka z dwóch
    private static readonly double PierwiastekZDwóch = Math.Sqrt(2);

    // Konstruktor
    public Prostokąt(double bokA, double bokB)
    {
        BokA = bokA;
        BokB = bokB;
    }

    // Publiczna statyczna metoda ArkuszPapieru
    public static Prostokąt ArkuszPapieru(string format)
    {
        if (string.IsNullOrWhiteSpace(format) || format.Length < 2)
        {
            throw new ArgumentException("Format arkusza papieru jest nieprawidłowy");
        }

        char X = format[0];
        if (!wysokośćArkusza0.ContainsKey(X))
        {
            throw new ArgumentException($"Niewłaściwy format: {X}. Oczekiwane 'A', 'B' lub 'C'");
        }

        if (!byte.TryParse(format.Substring(1), out byte i))
        {
            throw new ArgumentException("Nieprawidłowy indeks arkusza papieru.");
        }

        double wysokość = wysokośćArkusza0[X];
        double bokA = wysokość / Math.Pow(PierwiastekZDwóch, i);
        double bokB = bokA / PierwiastekZDwóch;

        return new Prostokąt(bokA, bokB);
    }

    public static void Main()
    {
        try
        {
            Prostokąt arkuszA4 = Prostokąt.ArkuszPapieru("A4");
            Console.WriteLine($"Arkusz A4: BokA = {arkuszA4.BokA:F2} mm, BokB = {arkuszA4.BokB:F2} mm");

            Prostokąt arkuszB1 = Prostokąt.ArkuszPapieru("B1");
            Console.WriteLine($"Arkusz B1: BokA = {arkuszB1.BokA:F2} mm, BokB = {arkuszB1.BokB:F2} mm");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

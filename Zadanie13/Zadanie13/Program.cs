using System;
using System.Linq;

public class Wektor
{
    private readonly double[] współrzędne;

    public Wektor(byte wymiar)
    {
        if (wymiar == 0)
        {
            throw new ArgumentException("Wymiar wektora musi być większy od zera.");
        }
        współrzędne = new double[wymiar];
    }

    public Wektor(params double[] współrzędne)
    {
        if (współrzędne == null || współrzędne.Length == 0)
        {
            throw new ArgumentException("Wektor musi mieć co najmniej jedną współrzędną.");
        }
        this.współrzędne = współrzędne;
    }

    public double Długość => Math.Sqrt(IloczynSkalarny(this, this));

    public byte Wymiar => (byte)współrzędne.Length;

    public double this[byte index]
    {
        get
        {
            if (index >= współrzędne.Length)
            {
                throw new IndexOutOfRangeException("Indeks poza zakresem wektora.");
            }
            return współrzędne[index];
        }
        set
        {
            if (index >= współrzędne.Length)
            {
                throw new IndexOutOfRangeException("Indeks poza zakresem wektora.");
            }
            współrzędne[index] = value;
        }
    }

    public static double IloczynSkalarny(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
        {
            throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");
        }

        double iloczyn = 0;
        for (int i = 0; i < V.Wymiar; i++)
        {
            iloczyn += V.współrzędne[i] * W.współrzędne[i];
        }
        return iloczyn;
    }

    public static Wektor Suma(params Wektor[] wektory)
    {
        if (wektory == null || wektory.Length == 0)
        {
            throw new ArgumentException("Musi być przynajmniej jeden wektor.");
        }

        byte wymiar = wektory[0].Wymiar;
        if (wektory.Any(w => w.Wymiar != wymiar))
        {
            throw new ArgumentException("Wszystkie wektory muszą mieć ten sam wymiar.");
        }

        double[] sumaWspółrzędnych = new double[wymiar];
        foreach (var wektor in wektory)
        {
            for (int i = 0; i < wymiar; i++)
            {
                sumaWspółrzędnych[i] += wektor.współrzędne[i];
            }
        }
        return new Wektor(sumaWspółrzędnych);
    }

    public static Wektor operator +(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
        {
            throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");
        }

        double[] sumaWspółrzędnych = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            sumaWspółrzędnych[i] = V.współrzędne[i] + W.współrzędne[i];
        }
        return new Wektor(sumaWspółrzędnych);
    }

    public static Wektor operator -(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
        {
            throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");
        }

        double[] różnicaWspółrzędnych = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            różnicaWspółrzędnych[i] = V.współrzędne[i] - W.współrzędne[i];
        }
        return new Wektor(różnicaWspółrzędnych);
    }

    public static Wektor operator *(Wektor V, double skalar)
    {
        double[] wynikWspółrzędnych = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynikWspółrzędnych[i] = V.współrzędne[i] * skalar;
        }
        return new Wektor(wynikWspółrzędnych);
    }

    public static Wektor operator *(double skalar, Wektor V)
    {
        return V * skalar;
    }

    public static Wektor operator /(Wektor V, double skalar)
    {
        if (skalar == 0)
        {
            throw new DivideByZeroException("Skalar nie może być zerem.");
        }

        double[] wynikWspółrzędnych = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynikWspółrzędnych[i] = V.współrzędne[i] / skalar;
        }
        return new Wektor(wynikWspółrzędnych);
    }

    public static void Main(string[] args)
    {
        Wektor wektor1 = new Wektor(1.0, 2.0, 3.0);
        Wektor wektor2 = new Wektor(4.0, 5.0, 6.0);

        Wektor suma = wektor1 + wektor2;
        Wektor różnica = wektor1 - wektor2;
        Wektor iloczynSkalarny = wektor1 * 2.0;
        Wektor iloczynSkalarny2 = 2.0 * wektor1;
        Wektor podzielony = wektor1 / 2.0;

        Console.WriteLine($"Suma: {string.Join(", ", suma.współrzędne)}");
        Console.WriteLine($"Różnica: {string.Join(", ", różnica.współrzędne)}");
        Console.WriteLine($"Iloczyn skalarny (wektor1 * 2.0): {string.Join(", ", iloczynSkalarny.współrzędne)}");
        Console.WriteLine($"Iloczyn skalarny (2.0 * wektor1): {string.Join(", ", iloczynSkalarny2.współrzędne)}");
        Console.WriteLine($"Podzielony: {string.Join(", ", podzielony.współrzędne)}");

        double długość = wektor1.Długość;
        Console.WriteLine($"Długość wektor1: {długość}");

        double iloczynSkalarnyValue = Wektor.IloczynSkalarny(wektor1, wektor2);
        Console.WriteLine($"Iloczyn skalarny wektor1 i wektor2: {iloczynSkalarnyValue}");

        Wektor sumaWektorów = Wektor.Suma(wektor1, wektor2, iloczynSkalarny);
        Console.WriteLine($"Suma wektorów: {string.Join(", ", sumaWektorów.współrzędne)}");
    }
}

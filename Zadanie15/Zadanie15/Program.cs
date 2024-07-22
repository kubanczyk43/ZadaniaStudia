using System;
using System.Collections.Generic;

public class Macierz<T> : IEquatable<Macierz<T>>
{
    private readonly T[,] macierz;
    public int Wiersze { get; }
    public int Kolumny { get; }

    public Macierz(int wiersze, int kolumny)
    {
        if (wiersze <= 0 || kolumny <= 0)
        {
            throw new ArgumentException("Wymiary macierzy muszą być większe od zera.");
        }

        Wiersze = wiersze;
        Kolumny = kolumny;
        macierz = new T[wiersze, kolumny];
    }

    public T this[int wiersz, int kolumna]
    {
        get
        {
            if (wiersz < 0 || wiersz >= Wiersze || kolumna < 0 || kolumna >= Kolumny)
            {
                throw new IndexOutOfRangeException("Indeksy są poza zakresem macierzy.");
            }

            return macierz[wiersz, kolumna];
        }
        set
        {
            if (wiersz < 0 || wiersz >= Wiersze || kolumna < 0 || kolumna >= Kolumny)
            {
                throw new IndexOutOfRangeException("Indeksy są poza zakresem macierzy.");
            }

            macierz[wiersz, kolumna] = value;
        }
    }

    public static bool operator ==(Macierz<T> m1, Macierz<T> m2)
    {
        if (ReferenceEquals(m1, m2))
        {
            return true;
        }

        if (ReferenceEquals(m1, null) || ReferenceEquals(m2, null))
        {
            return false;
        }

        if (m1.Wiersze != m2.Wiersze || m1.Kolumny != m2.Kolumny)
        {
            return false;
        }

        EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        for (int i = 0; i < m1.Wiersze; i++)
        {
            for (int j = 0; j < m1.Kolumny; j++)
            {
                if (!comparer.Equals(m1[i, j], m2[i, j]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool operator !=(Macierz<T> m1, Macierz<T> m2) => !(m1 == m2);

    public override bool Equals(object obj)
    {
        if (obj is Macierz<T> macierz)
        {
            return this == macierz;
        }

        return false;
    }

    public bool Equals(Macierz<T> other) => this == other;

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var item in macierz)
        {
            hash = hash * 31 + EqualityComparer<T>.Default.GetHashCode(item);
        }
        return hash;
    }
}

public class Program
{
    public static void Main()
    {
        Macierz<int> macierz1 = new Macierz<int>(2, 3);
        Macierz<int> macierz2 = new Macierz<int>(2, 3);

        macierz1[0, 0] = 1;
        macierz1[0, 1] = 2;
        macierz1[0, 2] = 3;
        macierz1[1, 0] = 4;
        macierz1[1, 1] = 5;
        macierz1[1, 2] = 6;

        macierz2[0, 0] = 1;
        macierz2[0, 1] = 2;
        macierz2[0, 2] = 3;
        macierz2[1, 0] = 4;
        macierz2[1, 1] = 5;
        macierz2[1, 2] = 6;

        Console.WriteLine(macierz1 == macierz2); 
        Console.WriteLine(macierz1.Equals(macierz2)); 
        Console.WriteLine(macierz1 != macierz2); 

        macierz2[1, 2] = 7;

        Console.WriteLine(macierz1 == macierz2); 
        Console.WriteLine(macierz1.Equals(macierz2)); 
        Console.WriteLine(macierz1 != macierz2);
    }
}

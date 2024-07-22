using System;
using System.Data;

public class Osoba
{

    private string imie;
    private string nazwisko;

    public DateTime? DataUrodzenia { get; set; } = null;
    public DateTime? DataSmierci { get; set; } = null;

    public Osoba(string imieNazwisko)
    {
        imieNazwisko = imieNazwisko;
    }

    public string Imie
    {
        get => imie;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Imię nie może być puste");
            }
            imie = value;
        }
    }

    public string ImieNazwisko
    {
        get => $"{Imie} {nazwisko}".Trim();
        set
        {
            var parts = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                Imie = parts[0];
                nazwisko = parts.Length > 1 ? parts[^1] : string.Empty;
            }
            else
            {
                throw new ArgumentException("Imię i Nazwisko nie może być puste");
            }
        }
    }

    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }

    public TimeSpan? Wiek
    {
        get
        {
            if (!DataUrodzenia.HasValue)
            {
                return null;
            }
            DateTime koniec = DataSmierci ?? DateTime.Now;
            return koniec - DataUrodzenia.Value;
        }
    }

    public class Program 
    {
        public static void Main() 
        {
            try 
            {
                Osoba osoba = new Osoba("Jakub Potrykus");
                osoba.DataUrodzenia = new DateTime(2003, 10, 6);
                Console.WriteLine($"Imię: {osoba.Imie}");
                Console.WriteLine($"Nazwisko: {osoba.Nazwisko}");
                Console.WriteLine($"ImięNazwisko: {osoba.ImieNazwisko}");
                Console.WriteLine($"Wiek: {osoba.Wiek?.Days / 365} lat");

                osoba.ImieNazwisko = "Jakub Potrykus";
                Console.WriteLine($"Nowe imię: {osoba.Imie}");
                Console.WriteLine($"Nowe nazwisko: {osoba.Nazwisko}");
                Console.WriteLine($"Nowe ImięNazwisko: {osoba.ImieNazwisko}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        
        }
    }
}

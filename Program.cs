using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

class Student
{
    public required string Ime { get; set; }
    public required string Prezime { get; set; }
    public required string Indeks { get; set; }
    public List<int> Ocjene { get; set; } = new List<int>();

    public double Prosjek()
    {
        return Ocjene.Count > 0 ? Ocjene.Average() : 0;
    }
}

class Program
{
    static Dictionary<string, Student> studenti = new Dictionary<string, Student>();
    static string fajl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "studenti.json");

    static void Main()
    {
        UcitajStudenteIzFajla();
        bool radi = true;

        while (radi)
        {
            Console.WriteLine("\n=== Meni ===");
            Console.WriteLine("1. Unesi studenta");
            Console.WriteLine("2. Unesi ocjenu studentu");
            Console.WriteLine("3. Prikazi sve studente");
            Console.WriteLine("4. Prikazi prosjek ocjena po studentu");
            Console.WriteLine("5. Obrisi studenta");
            Console.WriteLine("6. Pretrazi po indeksu");
            Console.WriteLine("7. Zatvori program");
            Console.Write("Izbor: ");

            string izbor = Console.ReadLine();

            switch (izbor)
            {
                case "1":
                    UnesiStudenta();
                    break;
                case "2":
                    UnesiOcjenu();
                    break;
                case "3":
                    PrikaziSveStudente();
                    break;
                case "4":
                    PrikaziProsjeke();
                    break;
                case "5":
                    ObrisiStudenta();
                    break;
                case "6":
                    PretraziStudenta();
                    break;
                case "7":
                    SpremiStudenteUFajl();
                    radi = false;
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija.");
                    break;
            }
        }
    }

    static void UnesiStudenta()
    {
        Console.Write("Ime: ");
        string ime = Console.ReadLine()?.Trim();

        Console.Write("Prezime: ");
        string prezime = Console.ReadLine()?.Trim();

    Console.Write("Indeks: ");
    string indeks = Console.ReadLine()?.Trim();

    if (indeks.Length > 0 && !studenti.ContainsKey(indeks) && ime.Length > 0 && prezime.Length > 0)
    {
        studenti[indeks] = new Student { Ime = ime, Prezime = prezime, Indeks = indeks };
        Console.WriteLine("Student uspješno unesen.");
    }
    else
    {
        Console.WriteLine("Student sa tim indeksom već postoji.");
    }
}

static void UnesiOcjenu()
{
    Console.Write("Unesi indeks studenta: ");
    string indeks = Console.ReadLine();

    if (studenti.ContainsKey(indeks))
    {
        Console.Write("Unesi ocjenu (6-10): ");
        if (int.TryParse(Console.ReadLine(), out int ocjena) && ocjena >= 6 && ocjena <= 10)
        {
            studenti[indeks].Ocjene.Add(ocjena);
            Console.WriteLine("Ocjena unesena.");
        }
        else
        {
            Console.WriteLine("Neispravna ocjena.");
        }
    }
    else
    {
        Console.WriteLine("Student nije pronađen.");
    }
}

static void PrikaziSveStudente()
{
    if (studenti.Count == 0)
    {
        Console.WriteLine("Nema unesenih studenata.");
        return;
    }

    foreach (var s in studenti.Values)
    {
        Console.WriteLine($"[{s.Indeks}] {s.Ime} {s.Prezime} - Ocjene: {string.Join(", ", s.Ocjene)}");
    }
}

static void PrikaziProsjeke()
{
    if (studenti.Count == 0)
    {
        Console.WriteLine("Nema unesenih studenata.");
        return;
    }

    foreach (var s in studenti.Values)
    {
        Console.WriteLine($"[{s.Indeks}] {s.Ime} {s.Prezime} - Prosjek: {s.Prosjek():0.00}");
    }
}

static void ObrisiStudenta()
{
    Console.Write("Unesi indeks studenta za brisanje: ");
    string indeks = Console.ReadLine();

    if (studenti.Remove(indeks))
    {
        Console.WriteLine("Student obrisan.");
    }
    else
    {
        Console.WriteLine("Student nije pronađen.");
    }
}

static void PretraziStudenta()
{
    Console.Write("Unesi indeks: ");
    string indeks = Console.ReadLine();

    if (studenti.TryGetValue(indeks, out var s))
    {
        Console.WriteLine($"[{s.Indeks}] {s.Ime} {s.Prezime} - Ocjene: {string.Join(", ", s.Ocjene)} - Prosjek: {s.Prosjek():0.00}");
    }
    else
    {
        Console.WriteLine("Student nije pronađen.");
    }
}

static void SpremiStudenteUFajl()
{
    string punaPutanja = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "studenti.json");
    var json = JsonSerializer.Serialize(studenti, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(punaPutanja, json);
    Console.WriteLine($"Podaci su sačuvani u fajl:\n{punaPutanja}");
}


static void UcitajStudenteIzFajla()
{
    if (File.Exists(fajl))
    {
        var json = File.ReadAllText(fajl);
        studenti = JsonSerializer.Deserialize<Dictionary<string, Student>>(json) ?? new Dictionary<string, Student>();
    }
}
}

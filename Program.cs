using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

// Klasa Student predstavlja jednog studenta i njegove ocjene
class Student
{
    public required string Ime { get; set; }      // Ime studenta
    public required string Prezime { get; set; }  // Prezime studenta
    public required string Indeks { get; set; }   // Broj indeksa
    public List<int> Ocjene { get; set; } = new List<int>(); // Lista ocjena

    // Metoda koja računa prosjek ocjena
    public double Prosjek()
    {
        return Ocjene.Count > 0 ? Ocjene.Average() : 0;
    }
}

class Program
{
    // Glavni kontejner svih studenata (ključ je indeks)
    static Dictionary<string, Student> studenti = new Dictionary<string, Student>();

    // Putanja do JSON fajla za čuvanje podataka ./lokacija_koda/bin/Debug/netX.X/studenti.json
    static string fajl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "studenti.json");

    static void Main()
    {
        UcitajStudenteIzFajla(); // Učitavanje postojećih podataka

        bool radi = true;
        while (radi)
        {
            // Glavni meni
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
                    SpremiStudenteUFajl(); // Spremanje na izlazu
                    radi = false;
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija.");
                    break;
            }
        }
    }

    // Dodavanje novog studenta
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
            Console.WriteLine("Student sa tim indeksom već postoji ili su podaci neispravni.");
        }
    }

    // Unos ocjene postojećem studentu
    static void UnesiOcjenu()
    {
        Console.Write("Unesi indeks studenta: ");
        string indeks = Console.ReadLine();

        if (studenti.ContainsKey(indeks))
        {
            Console.Write("Unesi ocjenu (5-10): ");
            if (int.TryParse(Console.ReadLine(), out int ocjena) && ocjena >= 5 && ocjena <= 10)
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

    // Prikaz svih studenata i njihovih ocjena
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

    // Prikaz prosjeka ocjena za svakog studenta
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

    // Brisanje studenta po indeksu
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

    // Pretraga studenta po indeksu
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

    // Spremanje podataka u JSON fajl
    static void SpremiStudenteUFajl()
    {

        var json = JsonSerializer.Serialize(studenti, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fajl, json);
        Console.WriteLine($"Podaci su sačuvani u fajl:\n{fajl}");
    }

    // Učitavanje podataka iz JSON fajla
    static void UcitajStudenteIzFajla()
    {
        if (File.Exists(fajl))
        {
            var json = File.ReadAllText(fajl);
            studenti = JsonSerializer.Deserialize<Dictionary<string, Student>>(json) ?? new Dictionary<string, Student>();
        }
    }
}

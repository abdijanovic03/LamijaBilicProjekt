# Studentski Menadžer - C# konzolna aplikacija

Ova aplikacija omogućava unos, pregled, uređivanje i čuvanje informacija o studentima i njihovim ocjenama. Program radi iz komandne linije i koristi JSON fajl za trajno čuvanje podataka.

## Opis aplikacije

Program koristi klasu `Student` za predstavljanje svakog studenta. Student ima sljedeće atribute:
- Ime (string)
- Prezime (string)
- Indeks (string, koristi se kao jedinstveni ID)
- Lista ocjena (List<int>)

Metoda `Prosjek()` računa prosječnu ocjenu studenta.

Podaci o studentima se čuvaju u `Dictionary<string, Student>`, gdje je ključ indeks studenta.

## Kratko objašnjenje C# konstrukcija

- `get; set;` su automatski generisane metode za čitanje i postavljanje vrijednosti svojstva. To je standardni C# način za definiranje pristupa varijabli.

- `JsonSerializer.Serialize()` i `Deserialize()` iz `System.Text.Json` se koriste za čuvanje i učitavanje podataka iz JSON fajla.

## Funkcionalnosti

1. Unos novog studenta
2. Dodavanje ocjena studentu
3. Prikaz svih studenata i njihovih ocjena
4. Prikaz prosjeka ocjena po studentu
5. Brisanje studenta prema indeksu
6. Pretraga studenta po indeksu
7. Spremanje podataka u fajl i izlaz iz programa




Za tebe:

## Kako pokrenuti aplikaciju

### Visual Studio:
Instaliraj prvo visual studio ako nemas
https://visualstudio.microsoft.com/vs/
Community ovu verziju

1. Kreiraj novi projekt tipa Console App (.NET)
2. U fajl `Program.cs` zalijepi sav kod aplikacije
3. Pokreni program klikom na "Start" ili tipkom Ctrl+F5


## Gdje se podaci čuvaju

Podaci se čuvaju u fajlu pod nazivom `studenti.json` koji se kreira automatski u direktoriju aplikacije (`bin/Debug/netX.X/studenti.json`).

## Napomena

Podaci su trajni sve dok se ne izbriše fajl `studenti.json`. Program je zamišljen kao lokalna jednostavna aplikacija bez baze podataka.

JSON (JavaScript Object Notation) je format za zapisivanje podataka kao tekst, ovo je koristeno jer je jednostavno i brze se postavi nego baza podataka, JSON je svakako standard za razmjenu podataka svugdje u web programiranju.Ako te zanima kako izlgleda upali program dodaj par studenta i ocjena jednostavno je

Sve ostalo pitaj ako sam sta zaboravio srentno <3 
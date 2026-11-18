# OISISI projekat – Sajam knjiga

Fakultetski projekat iz predmeta Osnovi informacionih sistema i softverskog inženjerstva (OISISI), Fakultet tehničkih nauka, Novi Sad.

Cilj projekta je razvoj informacionog sistema za upravljanje sajmom knjiga, korišćenjem programskog jezika C# i WPF tehnologije.

## Članovi tima

- **Student 1:** Mihailo Pljevaljičić (GitHub: @mihailop04)
- **Student 2:** Mile Ivanić (GitHub: @milkins1)

Rad na projektu je podeljen prema specifikaciji.

## Struktura projekta

Na početku rada napravljena je osnovna struktura projekta prema dokumentu *Predlog strukture projekta*.

Projekat je organizovan u tri dela:

- **Core** – modeli podataka, poslovna logika i rad sa podacima.
- **WpfClient** – grafički korisnički interfejs aplikacije.
- **ConsoleClient** – konzolni korisnički interfejs.

U okviru Core projekta kreirani su početni fajlovi za svih šest modela, kao i direktorijumi za dalji razvoj aplikacije.

## Trenutno stanje

- Napravljen zajednički GitHub repozitorijum.
- Kreiran Visual Studio Solution sa tri povezana projekta.
- Postavljena osnovna struktura direktorijuma i modela.
- Uspešno izvršen Build svih projekata.
- Provereno pokretanje početne WPF aplikacije.

## Implementacija adresa - Mile Ivanić

- Implementiran model Adresa sa CSV serijalizacijom i deserijalizacijom.
- Dodati IAdresaDao, AdresaDao i AdresaService.
- Implementirani i testirani konzolni prikaz,dodavanje,izmena i brisanje adresa.
- Adrese su povezane sa glavnim konzolnim menijem.
- Dodate provere obaveznih polja i nedozvoljenih znakova.


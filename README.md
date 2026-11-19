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

## Zajednička serijalizacija - Mihailo Pljevaljičić

- Implementirani ISerializable, Serializer<T> i Storage<T> po uzoru na primer CRUDExample sa vežbi.
- Podaci se čuvaju u CSV fajlovima u folderu Data, a putanje se grade pomoću Path.Combine.
- Dodate konverzije za datum, enum, listu ID-jeva i vrednost koja može biti prazna.

## Implementacija posetilaca - Mihailo Pljevaljičić

- Implementirani model Posetilac i enum Status sa CSV serijalizacijom i deserijalizacijom.
- Dodati IPosetilacDao, PosetilacDao i PosetilacService.
- Implementirani i testirani konzolni prikaz, dodavanje, izmena i brisanje posetilaca.
- Posetioci su povezani sa glavnim konzolnim menijem.
- Dodate provere obaveznih polja, jedinstvenog broja članske karte i nedozvoljenih znakova.
- Prosečna ocena posetioca se računa iz njegovih kupovina.
- Brisanjem posetioca brišu se i njegove kupovine.

## Implementacija kupovina - Mihailo Pljevaljičić

- Implementiran model Kupovina sa CSV serijalizacijom i deserijalizacijom.
- Dodati IKupovinaDao, KupovinaDao i KupovinaService.
- Implementirani i testirani konzolni prikaz, dodavanje, izmena i brisanje kupovina.
- Kupovine su povezane sa glavnim konzolnim menijem.
- Dodate provere da posetilac i knjiga postoje, da je ocena od 1 do 5 i da datum nije u budućnosti.

## Implementacija izdavača - Mihailo Pljevaljičić

- Implementiran model Izdavac sa CSV serijalizacijom i deserijalizacijom.
- Dodati IIzdavacDao, IzdavacDao i IzdavacService.
- Implementirani i testirani konzolni prikaz, dodavanje, izmena i brisanje izdavača.
- Izdavači su povezani sa glavnim konzolnim menijem.
- Dodate provere obaveznih polja, jedinstvene šifre i nedozvoljenih znakova.

## Konzolni deo - Mihailo Pljevaljičić

- Implementirani ConsoleViewUtils, PosetilacController, KupovinaController i IzdavacController.
- ConsoleViewUtils bezbedno čita brojeve, datume i tekst iz konzole.
- Program.cs povezuje DAO, servise i kontrolere i pokreće glavni meni.


## Implementacija adresa - Mile Ivanić

- Implementiran model Adresa sa CSV serijalizacijom i deserijalizacijom.
- Dodati IAdresaDao, AdresaDao i AdresaService.
- Implementirani i testirani konzolni prikaz,dodavanje,izmena i brisanje adresa.
- Adrese su povezane sa glavnim konzolnim menijem.
- Dodate provere obaveznih polja i nedozvoljenih znakova.

## Implementacija autora - Mile Ivanić

- Implementiran model Autor sa CSV serijalizacijom i deserijalizacijom.
- Dodati IAutorDao, AutorDao i AutorService.
- Implementirani i testirani konzolni prikaz,dodavanje,izmena i brisanje autora.
- Autori su povezane sa glavnim konzolnim menijem.
- Dodate provere obaveznih polja i nedozvoljenih znakova.

## Implementacija knjige - Mile Ivanić

- Implementiran model Knjiga sa CSV serijalizacijom i deserijalizacijom.
- Dodati IKnjigaDao, KnjigaDao i KnjigaService.
- Implementirani i testirani konzolni prikaz,dodavanje,izmena i brisanje knjige.
- Knjige su povezane sa glavnim konzolnim menijem.
- Implementirano uskladjivanje veza knjige sa autorima i izdavačima.
- Dodate provere obaveznih polja i nedozvoljenih znakova.

# Mini-bibliotekssystem

En konsolapplikation för ett litet bibliotek, gjord i C# / .NET 10 som skolprojekt.

## Funktioner

**Admin**
- Lägga till en bok
- Ta bort en bok (inte om den är utlånad)
- Visa alla böcker

**Låntagare**
- Söka efter böcker
- Låna en bok
- Lämna tillbaka en bok

Böcker och användare sparas i JSON-filer när man väljer "Spara och avsluta".

## Köra programmet

1. Klona repot: `git clone https://github.com/Omar-Alissa/mini-bibliotekssystem.git`
2. Öppna projektet i Visual Studio
3. Tryck på Start (F5)

## Testkonton

- **Admin:** användarnamn `admin`, lösenord `admin123`
- **Låntagare:** användarnamn `låntagare`, lösenord `lösen123`

## Klasser

- **Person** (abstrakt): basklass, Admin och Låntagare ärver från den
- **Bok**: titel, författare, status och vem som har lånat den
- **Bibliotek**: inloggning, sökning (LINQ) och ändringar
- **FilHanterare**: läser och sparar JSON
- **Meny**: all inmatning och utskrift i konsolen

## Tekniker

C#, .NET 10, OOP (arv, abstrakt klass), LINQ, System.Text.Json, Git

## Begränsningar

- Lösenorden sparas som vanlig text
- Data sparas bara när man väljer "Spara och avsluta"
- Det går inte att skapa nya användare eller ändra böcker
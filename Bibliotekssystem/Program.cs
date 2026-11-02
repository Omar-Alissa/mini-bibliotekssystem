using Bibliotekssystem.Models;

Bok bok = new Bok(1, "Sagan om ringen", "Tolkien");
Console.WriteLine(bok);

bok.Låna("omar");
Console.WriteLine(bok);
Console.WriteLine(bok.Låntagare);

bok.LämnaTillbaka();
Console.WriteLine(bok);
Console.ReadKey();
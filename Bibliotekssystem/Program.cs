using Bibliotekssystem;
using Bibliotekssystem.Models;

FilHanterare filHanterare = new FilHanterare();
List<Bok> böcker = filHanterare.LäsInBöcker();
List<Person> användare = filHanterare.LäsInAnvändare();

Bibliotek bibliotek = new Bibliotek(böcker, användare);
bibliotek.SkapaStandardKonton();

Meny meny = new Meny(bibliotek, filHanterare);
meny.KörHuvudmeny();

Console.ReadKey();
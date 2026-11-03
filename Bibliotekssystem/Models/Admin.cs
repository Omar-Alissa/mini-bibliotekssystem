namespace Bibliotekssystem.Models
{
    public class Admin : Person
    {
        // rollen för admin
        public override string Roll => "Admin";

        // tom konstruktor för JSON
        public Admin()
        {
        }

        // skickar namn och lösenord till Person
        public Admin(string användarnamn, string lösenord) : base(användarnamn, lösenord)
        {
        }

        // bara admin kan lägga till böcker
        public bool LäggTillBok(Bibliotek bibliotek, string titel, string författare)
        {
            return bibliotek.LäggTillBok(titel, författare);
        }

        // bara admin kan ta bort böcker
        public void TaBortBok(Bibliotek bibliotek, Bok bok)
        {
            bibliotek.TaBortBok(bok);
        }
    }
}
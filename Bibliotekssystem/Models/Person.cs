namespace Bibliotekssystem.Models
{
    public abstract class Person
    {
        // användarens namn och lösenord
        public string Användarnamn { get; set; } = string.Empty;
        public string Lösenord { get; set; } = string.Empty;

        // admin eller låntagare, varje klass sätter sin roll
        public abstract string Roll { get; }

        // tom konstruktor för att JSON ska kunna läsa filen
        protected Person()
        {
        }

        // konstruktor när vi skapar en ny användare
        protected Person(string användarnamn, string lösenord)
        {
            // kollar att namnet inte är tomt
            if (string.IsNullOrWhiteSpace(användarnamn))
            {
                throw new ArgumentException("Användarnamnet får inte vara tomt.");
            }

            // kollar att lösenordet inte är tomt
            if (string.IsNullOrWhiteSpace(lösenord))
            {
                throw new ArgumentException("Lösenordet får inte vara tomt.");
            }

            Användarnamn = användarnamn;
            Lösenord = lösenord;
        }

        // kollar om lösenordet är rätt
        public bool KontrolleraLösenord(string lösenord)
        {
            return Lösenord == lösenord;
        }
        // TODO: aktivera när klassen Bibliotek finns
        // alla användare kan söka böcker
        /*  public List<Bok> SökBöcker(Bibliotek bibliotek, string sökord)
          {
              return bibliotek.SökBöcker(sökord);
          }
        */
    }
}
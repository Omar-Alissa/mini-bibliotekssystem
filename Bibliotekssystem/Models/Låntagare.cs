namespace Bibliotekssystem.Models
{
    public class Låntagare : Person
    {
        // rollen för låntagare
        public override string Roll => "Låntagare";

        // tom konstruktor för JSON
        public Låntagare()
        {
        }

        // skickar namn och lösenord till Person
        public Låntagare(string användarnamn, string lösenord) : base(användarnamn, lösenord)
        {
        }

        // låntagaren lånar boken med sitt eget namn
        public void LånaBok(Bok bok)
        {
            bok.Låna(Användarnamn);
        }

        // låntagaren lämnar tillbaka boken
        public void LämnaTillbakaBok(Bok bok)
        {
            bok.LämnaTillbaka();
        }
        // visar bara böcker som denna låntagare har lånat
          public List<Bok> HämtaMinaLån(Bibliotek bibliotek)
          {
              return bibliotek.HämtaLånadeBöcker(Användarnamn);
          }
    }
} 
using Bibliotekssystem.Models;

namespace Bibliotekssystem
{
    public class Bibliotek
    {
        // listor med böcker och användare
        public List<Bok> Böcker { get; set; }
        public List<Person> Användare { get; set; }

        // tar emot listorna från filen
        public Bibliotek(List<Bok> böcker, List<Person> användare)
        {
            Böcker = böcker;
            Användare = användare;
        }

        // logga in, returnerar null om det blir fel
        public Person? LoggaIn(string användarnamn, string lösenord)
        {
            return Användare.FirstOrDefault(p =>
                p.Användarnamn == användarnamn && p.KontrolleraLösenord(lösenord));
        }

        // första gången skapas en admin och en låntagare
        public void SkapaStandardKonton()
        {
            if (!Användare.Any())
            {
                Användare.Add(new Admin("admin", "admin123"));
                Användare.Add(new Låntagare("låntagare", "lösen123"));
            }
        }

        // lägg till bok, false om den redan finns
        public bool LäggTillBok(string titel, string författare)
        {
            bool finns = Böcker.Any(b =>
                b.Titel.Equals(titel, StringComparison.OrdinalIgnoreCase) &&
                b.Författare.Equals(författare, StringComparison.OrdinalIgnoreCase));
            if (finns)
            {
                return false;
            }

            Böcker.Add(new Bok(SkapaNyttId(), titel, författare));
            return true;
        }

        // ta bort bok
        public void TaBortBok(Bok bok)
        {
            Böcker.Remove(bok);
        }

        // hitta bok med id
        public Bok? HittaBokMedId(int id)
        {
            return Böcker.FirstOrDefault(b => b.Id == id);
        }

        // hitta bok med titel
        public Bok? HittaBokMedTitel(string titel)
        {
            return Böcker.FirstOrDefault(b =>
                b.Titel.Equals(titel, StringComparison.OrdinalIgnoreCase));
        }

        // sök på titel eller författare
        public List<Bok> SökBöcker(string sökord)
        {
            return Böcker
                .Where(b => b.Titel.Contains(sökord, StringComparison.OrdinalIgnoreCase) ||
                            b.Författare.Contains(sökord, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // böcker som användaren har lånat
        public List<Bok> HämtaLånadeBöcker(string användarnamn)
        {
            return Böcker.Where(b => b.Låntagare == användarnamn).ToList();
        }

        // nytt id, tar det högsta och plus 1
        private int SkapaNyttId()
        {
            // Max ger fel om listan är tom
            if (!Böcker.Any())
            {
                return 1;
            }

            return Böcker.Max(b => b.Id) + 1;
        }
    }
}
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
            foreach (Person person in Användare)
            {
                if (person.Användarnamn == användarnamn && person.KontrolleraLösenord(lösenord))
                {
                    return person;
                }
            }

            return null;
        }

        // första gången skapas en admin och en låntagare
        public void SkapaStandardKonton()
        {
            if (Användare.Count == 0)
            {
                Användare.Add(new Admin("admin", "admin123"));
                Användare.Add(new Låntagare("låntagare", "lösen123"));
            }
        }

        // lägg till bok, false om den redan finns
        public bool LäggTillBok(string titel, string författare)
        {
            foreach (Bok bok in Böcker)
            {
                if (bok.Titel.Equals(titel, StringComparison.OrdinalIgnoreCase) &&
                    bok.Författare.Equals(författare, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
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
            foreach (Bok bok in Böcker)
            {
                if (bok.Id == id)
                {
                    return bok;
                }
            }

            return null;
        }

        // hitta bok med titel
        public Bok? HittaBokMedTitel(string titel)
        {
            foreach (Bok bok in Böcker)
            {
                if (bok.Titel.Equals(titel, StringComparison.OrdinalIgnoreCase))
                {
                    return bok;
                }
            }

            return null;
        }

        // sök på titel eller författare
        public List<Bok> SökBöcker(string sökord)
        {
            List<Bok> resultat = new List<Bok>();

            foreach (Bok bok in Böcker)
            {
                if (bok.Titel.Contains(sökord, StringComparison.OrdinalIgnoreCase) ||
                    bok.Författare.Contains(sökord, StringComparison.OrdinalIgnoreCase))
                {
                    resultat.Add(bok);
                }
            }

            return resultat;
        }

        // böcker som användaren har lånat
        public List<Bok> HämtaLånadeBöcker(string användarnamn)
        {
            List<Bok> lånade = new List<Bok>();

            foreach (Bok bok in Böcker)
            {
                if (bok.Låntagare == användarnamn)
                {
                    lånade.Add(bok);
                }
            }

            return lånade;
        }

        // nytt id, tar det högsta och plus 1
        private int SkapaNyttId()
        {
            int högsta = 0;

            foreach (Bok bok in Böcker)
            {
                if (bok.Id > högsta)
                {
                    högsta = bok.Id;
                }
            }

            return högsta + 1;
        }
    }
}
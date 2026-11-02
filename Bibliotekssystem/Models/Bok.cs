namespace Bibliotekssystem.Models
{
    public class Bok
    {
        // Egenskaper
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Författare { get; set; } = string.Empty;
        public BokStatus Status { get; set; } = BokStatus.Ledig;
        public string? Låntagare { get; set; }

        // Tom konstruktor, behövs när JSON läser in böcker från filen
        public Bok()
        {
        }

        // Konstruktor som används när en ny bok skapas i programmet
        public Bok(int id, string titel, string författare)
        {
            Id = id;
            Titel = titel;
            Författare = författare;
        }

        // Returnerar true om boken är ledig
        public bool ÄrLedig()
        {
            return Status == BokStatus.Ledig;
        }

        // Lånar ut boken till en användare
        public void Låna(string användarnamn)
        {
            Status = BokStatus.Utlånad;
            Låntagare = användarnamn;
        }

        // Lämnar tillbaka boken
        public void LämnaTillbaka()
        {
            Status = BokStatus.Ledig;
            Låntagare = null;
        }

        // Visar boken som en rad i menyn
        public override string ToString()
        {
            return $"{Id}. {Titel} - {Författare} ({Status})";
        }
    }
}
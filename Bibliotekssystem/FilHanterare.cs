using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bibliotekssystem.Models;

namespace Bibliotekssystem
{
    public class FilHanterare
    {
        private readonly string bokFil = "böcker.json";
        private readonly string användarFil = "användare.json";

        private readonly JsonSerializerOptions inställningar = new JsonSerializerOptions
        {
            WriteIndented = true,
            // annars blir å ä ö konstiga tecken i filen
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            // sparar status som text istället för siffra
            Converters = { new JsonStringEnumConverter() }
        };

        public List<Bok> LäsInBöcker()
        {
            if (!File.Exists(bokFil))
            {
                return new List<Bok>();
            }

            try
            {
                string json = File.ReadAllText(bokFil);
                return JsonSerializer.Deserialize<List<Bok>>(json, inställningar) ?? new List<Bok>();
            }
            catch (JsonException)
            {
                // trasig fil, börjar med tom lista
                return new List<Bok>();
            }
        }

        public List<Person> LäsInAnvändare()
        {
            if (!File.Exists(användarFil))
            {
                return new List<Person>();
            }

            try
            {
                string json = File.ReadAllText(användarFil);
                return JsonSerializer.Deserialize<List<Person>>(json, inställningar) ?? new List<Person>();
            }
            catch (JsonException)
            {
                return new List<Person>();
            }
        }

        public void Spara(List<Bok> böcker, List<Person> användare)
        {
            string bokJson = JsonSerializer.Serialize(böcker, inställningar);
            File.WriteAllText(bokFil, bokJson);

            string användarJson = JsonSerializer.Serialize(användare, inställningar);
            File.WriteAllText(användarFil, användarJson);
        }
    }
}
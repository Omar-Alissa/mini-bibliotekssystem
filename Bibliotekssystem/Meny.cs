using Bibliotekssystem.Models;

namespace Bibliotekssystem
{
	public class Meny
	{
		private Bibliotek bibliotek;
		private FilHanterare filHanterare;

		public Meny(Bibliotek bibliotek, FilHanterare filHanterare)
		{
			this.bibliotek = bibliotek;
			this.filHanterare = filHanterare;
		}

		public void KörHuvudmeny()
		{
			bool körs = true;

			while (körs)
			{
				Console.WriteLine();
				Console.WriteLine("Huvudmeny");
				Console.WriteLine("1. Logga in");
				Console.WriteLine("2. Avsluta");
				Console.Write("Välj: ");
				string val = Console.ReadLine() ?? "";

				switch (val)
				{
					case "1":
						Person? person = LoggaIn();

						// typen bestämmer vilken meny som visas
						if (person is Admin admin)
						{
							AdminMeny(admin);
						}
						else if (person is Låntagare låntagare)
						{
							LåntagareMeny(låntagare);
						}
						break;

					case "2":
						filHanterare.Spara(bibliotek.Böcker, bibliotek.Användare);
						Console.WriteLine("Data sparad. Hej då!");
						körs = false;
						break;

					default:
						Console.WriteLine("Ogiltigt val, välj 1 eller 2");
						break;
				}
			}
		}

		private Person? LoggaIn()
		{
			int antalFörsök = 0;

			while (antalFörsök < 3)
			{
				Console.Write("Användarnamn: ");
				string namn = Console.ReadLine() ?? "";
				Console.Write("Lösenord: ");
				string lösenord = Console.ReadLine() ?? "";

				// tomma fält räknas inte som försök
				if (namn == "" || lösenord == "")
				{
					Console.WriteLine("Fälten får inte vara tomma");
					continue;
				}

				Person? person = bibliotek.LoggaIn(namn, lösenord);

				if (person != null)
				{
					Console.WriteLine("Välkommen " + person.Användarnamn + " (" + person.Roll + ")");
					return person;
				}

				antalFörsök++;

				if (antalFörsök < 3)
				{
					Console.WriteLine("Felaktiga uppgifter");
				}
			}

			Console.WriteLine("För många försök");
			return null;
		}

		private void AdminMeny(Admin admin)
		{
			bool inloggad = true;

			while (inloggad)
			{
				Console.WriteLine();
				Console.WriteLine("Adminmeny");
				Console.WriteLine("1. Lägg till bok");
				Console.WriteLine("2. Ta bort bok");
				Console.WriteLine("3. Visa alla böcker");
				Console.WriteLine("4. Logga ut");
				Console.Write("Välj: ");
				string val = Console.ReadLine() ?? "";

				switch (val)
				{
					case "1":
						LäggTillBok(admin);
						break;
					case "2":
						TaBortBok(admin);
						break;
					case "3":
						VisaAllaBöcker();
						break;
					case "4":
						Console.WriteLine("Du har loggat ut");
						inloggad = false;
						break;
					default:
						Console.WriteLine("Ogiltigt val, välj 1-4");
						break;
				}
			}
		}

		private void LäggTillBok(Admin admin)
		{
			Console.Write("Titel: ");
			string titel = Console.ReadLine() ?? "";
			Console.Write("Författare: ");
			string författare = Console.ReadLine() ?? "";

			while (titel == "" || författare == "")
			{
				Console.WriteLine("Fälten får inte vara tomma");
				Console.Write("Titel: ");
				titel = Console.ReadLine() ?? "";
				Console.Write("Författare: ");
				författare = Console.ReadLine() ?? "";
			}

			if (admin.LäggTillBok(bibliotek, titel, författare))
			{
				Console.WriteLine("Boken har lagts till");
			}
			else
			{
				Console.WriteLine("Boken finns redan");
			}
		}

		private void TaBortBok(Admin admin)
		{
			if (!bibliotek.Böcker.Any())
			{
				Console.WriteLine("Inga böcker finns");
				return;
			}

			VisaAllaBöcker();

			Console.Write("Skriv bokens ID: ");
			int id;

			// frågar igen tills det är ett heltal
			while (!int.TryParse(Console.ReadLine(), out id))
			{
				Console.WriteLine("Ange ett giltigt ID nummer");
				Console.Write("Skriv bokens ID: ");
			}

			Bok? bok = bibliotek.HittaBokMedId(id);

			if (bok == null)
			{
				Console.WriteLine("Ingen bok med det ID");
				return;
			}

			if (!bok.ÄrLedig())
			{
				Console.WriteLine("Boken är utlånad och kan inte tas bort");
				return;
			}

			Console.Write("Är du säker? JA/NEJ: ");
			string svar = (Console.ReadLine() ?? "").ToUpper();

			while (svar != "JA" && svar != "NEJ")
			{
				Console.WriteLine("Svara med JA eller NEJ");
				Console.Write("Är du säker? JA/NEJ: ");
				svar = (Console.ReadLine() ?? "").ToUpper();
			}

			switch (svar)
			{
				case "JA":
					admin.TaBortBok(bibliotek, bok);
					Console.WriteLine("Boken har tagits bort");
					break;
				case "NEJ":
					Console.WriteLine("Borttagning avbruten");
					break;
			}
		}

		private void VisaAllaBöcker()
		{
			if (!bibliotek.Böcker.Any())
			{
				Console.WriteLine("Inga böcker finns");
				return;
			}

			foreach (Bok bok in bibliotek.Böcker.OrderBy(b => b.Id))
			{
				Console.WriteLine(bok);
			}
		}

		private void LåntagareMeny(Låntagare låntagare)
		{
			bool inloggad = true;

			while (inloggad)
			{
				Console.WriteLine();
				Console.WriteLine("Användarmeny");
				Console.WriteLine("1. Sök efter bok");
				Console.WriteLine("2. Låna bok");
				Console.WriteLine("3. Lämna tillbaka bok");
				Console.WriteLine("4. Logga ut");
				Console.Write("Välj: ");
				string val = Console.ReadLine() ?? "";

				switch (val)
				{
					case "1":
						SökBok(låntagare);
						break;
					case "2":
						LånaBok(låntagare);
						break;
					case "3":
						LämnaTillbaka(låntagare);
						break;
					case "4":
						Console.WriteLine("Du har loggat ut");
						inloggad = false;
						break;
					default:
						Console.WriteLine("Ogiltigt val, välj mellan 1-4");
						break;
				}
			}
		}

		private void SökBok(Låntagare låntagare)
		{
			if (!bibliotek.Böcker.Any())
			{
				Console.WriteLine("Inga böcker finns i biblioteket");
				return;
			}

			Console.Write("Skriv titel eller författare: ");
			string sökord = Console.ReadLine() ?? "";

			while (sökord == "")
			{
				Console.WriteLine("Fältet får inte vara tomt");
				Console.Write("Skriv titel eller författare: ");
				sökord = Console.ReadLine() ?? "";
			}

			List<Bok> resultat = låntagare.SökBöcker(bibliotek, sökord);

			if (!resultat.Any())
			{
				Console.WriteLine("Boken hittades inte");
				return;
			}

			foreach (Bok bok in resultat)
			{
				Console.WriteLine(bok);
			}
		}

		private void LånaBok(Låntagare låntagare)
		{
			Console.Write("Skriv bokens titel: ");
			string titel = Console.ReadLine() ?? "";

			while (titel == "")
			{
				Console.WriteLine("Fältet får inte vara tomt");
				Console.Write("Skriv bokens titel: ");
				titel = Console.ReadLine() ?? "";
			}

			Bok? bok = bibliotek.HittaBokMedTitel(titel);

			if (bok == null)
			{
				Console.WriteLine("Boken hittades inte");
			}
			else if (!bok.ÄrLedig())
			{
				Console.WriteLine("Boken är redan utlånad");
			}
			else
			{
				låntagare.LånaBok(bok);
				Console.WriteLine("Du har lånat boken");
			}
		}

		private void LämnaTillbaka(Låntagare låntagare)
		{
			List<Bok> mittLån = låntagare.HämtaMinaLån(bibliotek);

			if (!mittLån.Any())
			{
				Console.WriteLine("Du har inga lånade böcker");
				return;
			}

			Console.WriteLine("Dina lånade böcker:");
			foreach (Bok b in mittLån)
			{
				Console.WriteLine(b);
			}

			Console.Write("Skriv bokens titel: ");
			string titel = Console.ReadLine() ?? "";

			while (titel == "")
			{
				Console.WriteLine("Fältet får inte vara tomt");
				Console.Write("Skriv bokens titel: ");
				titel = Console.ReadLine() ?? "";
			}

			// letar bara bland användarens egna lån
			Bok? bok = mittLån.FirstOrDefault(b =>
				b.Titel.Equals(titel, StringComparison.OrdinalIgnoreCase));

			if (bok == null)
			{
				Console.WriteLine("Du har inte lånat den boken");
				return;
			}

			låntagare.LämnaTillbakaBok(bok);
			Console.WriteLine("Boken är återlämnad");
		}
	}
}
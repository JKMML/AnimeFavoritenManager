using AnimeFavoritenManager.Daten;
using System;

namespace AnimeFavoritenManager.Menu
{
    internal class Hauptmenu
    {
        private readonly BenutzerVerwalter benutzerVerwalter = new BenutzerVerwalter();
        private readonly AnimeDatenVerwalter animeDatenVerwalter = new AnimeDatenVerwalter();
        private readonly FavoritenMenu favoritenMenu;
        private bool anwendungLaeuft = true;

        public Hauptmenu()
        {
            // animeDatenVerwalter.DummyDatenLaden();

            favoritenMenu = new FavoritenMenu(benutzerVerwalter, animeDatenVerwalter);
        }

        public void AnwendungStarten()
        {
            AnimeDatenBeimStartLaden();

            while (anwendungLaeuft)
            {
                HauptmenuAnzeigen();
                string? benutzerAuswahl = Console.ReadLine();
                HauptmenuAuswahlBehandeln(benutzerAuswahl);
            }
        }

        private void HauptmenuAnzeigen()
        {
            Console.Clear();
            Console.WriteLine("=== Anime Favoriten Manager ===");
            Console.WriteLine("1. Benutzer Registrieren");

            if (!benutzerVerwalter.IstBenutzerAngemeldet)
            {
                Console.WriteLine("2. Benutzer Login");
            }
            else
            {
                Console.WriteLine("2. Benutzer Logout");
                Console.WriteLine("3. Favoriten-Menü");
            }

            Console.WriteLine("0. Beenden\n");

            if (benutzerVerwalter.AngemeldeterBenutzer != null)
            {
                Console.WriteLine($"(Angemeldet als: {benutzerVerwalter.AngemeldeterBenutzer.BenutzerName})");
            }
            Console.Write("Bitte wählen Sie eine Option: ");
        }

        private void HauptmenuAuswahlBehandeln(string? benutzerAuswahl)
        {
            switch (benutzerAuswahl)
            {
                case "1":
                    benutzerVerwalter.BenutzerRegistrieren();
                    break;

                case "2":
                    if (!benutzerVerwalter.IstBenutzerAngemeldet)
                    {
                       
                        benutzerVerwalter.BenutzerLogin();
                    }
                    else
                    {
                       
                        benutzerVerwalter.BenutzerLogout();
                    }
                    break;

                case "3":
                    if (benutzerVerwalter.AngemeldeterBenutzer != null)
                    {
                        favoritenMenu.FavoritenStarten();
                    }
                    else
                    {
                        Console.WriteLine("Bitte melden Sie sich an.");
                        Console.WriteLine("Weiter mit einer beliebigen Taste");
                        Console.ReadKey();
                    }
                    break;

                case "0":
                    AnwendungBeenden();
                    break;

                default:
                    UngueltigeAuswahlBehandeln();
                    break;
            }
        }

        private void AnwendungBeenden()
        {
            Console.WriteLine("Programm wird beendet...");
            anwendungLaeuft = false;
        }

        private void UngueltigeAuswahlBehandeln()
        {
            Console.WriteLine("Ungültige Auswahl. Bitte erneut versuchen.");
            Console.WriteLine("Weiter mit einer beliebigen Taste");
            Console.ReadKey();
        }

        private void AnimeDatenBeimStartLaden()
        {
            Console.Clear();
            Console.WriteLine("Anime-Daten werden von der API geladen...");

            try
            {
                animeDatenVerwalter.AnimeVonApiLadenAsync().GetAwaiter().GetResult();
                Console.WriteLine("Anime-Daten wurden erfolgreich geladen.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Laden der Anime-Daten:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Weiter mit einer beliebigen Taste...");
            Console.ReadKey();
        }
    }
}

using AnimeFavoritenManager.Daten;
using AnimeFavoritenManager.HelferKlassen;
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


                Console.Write("Bitte wählen: ");
                int auswahl = EingabeHelfer.LeseAuswahlZwischen(0, 2, true);

                HauptmenuAuswahlBehandeln(auswahl);
            }
        }

        private void HauptmenuAnzeigen()
        {
            {
                Console.Clear();
                FarbAusgabe.SchreibeTitel("=== Anime Favoriten Manager ===");
                Console.WriteLine();

                if (!benutzerVerwalter.IstBenutzerAngemeldet)
                {
                    Console.WriteLine("1. Registrieren");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("0. Beenden");
                }
                else
                {
                    Console.WriteLine("1. Favoriten-Menü");
                    Console.WriteLine("2. Logout");
                    Console.WriteLine("0. Beenden");

                    Console.WriteLine();
                    FarbAusgabe.SchreibeErfolg("Angemeldet als: " + benutzerVerwalter.AngemeldeterBenutzer.BenutzerName);
                }

                Console.WriteLine();
                Console.Write("Bitte wählen: ");
            }
        }

        private void HauptmenuAuswahlBehandeln(int benutzerAuswahl)
        {
            switch (benutzerAuswahl)
            {
                case 1:
                    if (!benutzerVerwalter.IstBenutzerAngemeldet)
                    {
                        // 1 = Registrieren (nicht eingeloggt)
                        benutzerVerwalter.BenutzerRegistrieren();
                    }
                    else
                    {
                        // 1 = Favoriten-Menü (eingeloggt)
                        favoritenMenu.FavoritenStarten();
                    }
                    break;

                case 2:
                    if (!benutzerVerwalter.IstBenutzerAngemeldet)
                    {
                        // 2 = Anmelden
                        benutzerVerwalter.BenutzerLogin();

                        // Wenn Login erfolgreich → direkt ins Favoriten-Menü
                        if (benutzerVerwalter.IstBenutzerAngemeldet)
                        {
                            favoritenMenu.FavoritenStarten();
                        }
                    }
                    else
                    {
                        // 2 = Abmelden
                        benutzerVerwalter.BenutzerLogout();
                    }
                    break;

                case 0:
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
            FarbAusgabe.SchreibeHinweis("Anime-Daten werden von der API geladen...");

            try
            {
                animeDatenVerwalter.AnimeVonApiLadenAsync().GetAwaiter().GetResult();
                FarbAusgabe.SchreibeErfolg("Anime-Daten wurden erfolgreich geladen.");
            }
            catch (Exception ex)
            {
                FarbAusgabe.SchreibeFehler("Fehler beim Laden der Anime-Daten:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Weiter mit einer beliebigen Taste...");
            Console.ReadKey();
        }
    }
}

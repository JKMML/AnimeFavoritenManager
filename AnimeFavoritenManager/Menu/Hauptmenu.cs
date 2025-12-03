using AnimeFavoritenManager.Daten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFavoritenManager.Menu
{
    internal class Hauptmenu
    {
        private readonly BenutzerVerwalter benutzerVerwalter = new BenutzerVerwalter();
        private bool anwendungLaeuft = true;

        public void AnwendungStarten()

        { while (anwendungLaeuft) 
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
            Console.WriteLine("2. Benutzer Login");
            if (benutzerVerwalter.AngemeldeterBenutzer!= null)
            {
                Console.WriteLine("3) Favoriten-Menü (kommt an Tag 3)");
                Console.WriteLine($"(Angemeldet als: {benutzerVerwalter.AngemeldeterBenutzer.BenutzerName})");
                Console.WriteLine("4 Anime Season Liste anzeigen");
            }
            Console.WriteLine("0. Beenden");
            Console.Write("Bitte wählen Sie eine Option: ");
        }
        private void HauptmenuAuswahlBehandeln(string? benutzerAuswahl)
        {
            switch (benutzerAuswahl)
            {
                case "1":
                    BenutzerRegistrierungStarten();
                    break;

                case "2":
                    BenutzerAnmeldungStarten();
                    break;
                case "3":
                    if (benutzerVerwalter.AngemeldeterBenutzer != null)
                    {
                        // Favoriten-Menü aufrufen (noch nicht implementiert)
                        Console.WriteLine("Favoriten-Menü wird bald verfügbar sein!");
                        Console.WriteLine("Weiter mit einer beliebigen Taste");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Bitte melden Sie sich zuerst an, um auf das Favoriten-Menü zuzugreifen.");
                        Console.WriteLine("Weiter mit einer beliebigen Taste");
                        Console.ReadKey();
                    }
                    break;
                case "4":
                    if (benutzerVerwalter.AngemeldeterBenutzer != null)
                    {
                        // Seasonal Anime aufrufen (noch nicht implementiert)
                        Console.WriteLine("AnimeListe wird bald verfügbar sein!");
                        Console.WriteLine("Weiter mit einer beliebigen Taste");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Bitte melden Sie sich zuerst an, um auf das Favoriten-Menü zuzugreifen.");
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

        private void BenutzerRegistrierungStarten()
        {
            benutzerVerwalter.BenutzerRegistrieren();
        }

        private void BenutzerAnmeldungStarten()
        {
            benutzerVerwalter.BenutzerLogin();
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFavoritenManager.Menu
{
    internal class Hauptmenu
    {
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
            Console.WriteLine("1. Benutzerverwaltung");
            Console.WriteLine("2. Anime Verwaltung");
            Console.WriteLine("3. Beenden");
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
                    AnwendungBeenden();
                    break;

                default:
                    UngueltigeAuswahlBehandeln();
                    break;
            }
        }

        private void BenutzerRegistrierungStarten()
        {
            Console.WriteLine("Registrierung Platzhalter");
            Console.ReadKey();
        }

        private void BenutzerAnmeldungStarten()
        {
            Console.WriteLine("Anmeldung Platzhalter");
            Console.ReadKey();
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

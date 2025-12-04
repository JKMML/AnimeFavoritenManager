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
        private readonly AnimeDatenVerwalter animeDatenVerwalter = new AnimeDatenVerwalter();
        private readonly FavoritenMenu favoritenMenu;
        private bool anwendungLaeuft = true;

        public Hauptmenu()
        {
            animeDatenVerwalter.DummyDatenLaden();
            favoritenMenu = new FavoritenMenu(benutzerVerwalter, animeDatenVerwalter);
        }

        public void AnwendungStarten()                   
        {
            

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
            Console.WriteLine("2. Benutzer Login");
            if (benutzerVerwalter.AngemeldeterBenutzer!= null)
            {
                Console.WriteLine("3) Favoriten-Menü");
                Console.WriteLine($"(Angemeldet als: {benutzerVerwalter.AngemeldeterBenutzer.BenutzerName})");
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
                        favoritenMenu.Starten();
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

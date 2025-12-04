using AnimeFavoritenManager.Daten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFavoritenManager.Menu
{
    internal class FavoritenMenu
    {
        private readonly BenutzerVerwalter benutzerVerwalter;
        private readonly AnimeDatenVerwalter animeDatenVerwalter;

        public FavoritenMenu(BenutzerVerwalter benutzerVerwalter, AnimeDatenVerwalter animeDatenVerwalter)
        {
            this.benutzerVerwalter = benutzerVerwalter;
            this.animeDatenVerwalter = animeDatenVerwalter;
        }

        public void Starten()
        {
            bool zurueck = false;

            while (!zurueck)
            {
                Console.Clear();
                Console.WriteLine("=== Favoriten-Menü ===");
                Console.WriteLine("1. Anime-Liste anzeigen");
                Console.WriteLine("2. Anime zu Favoriten hinzufügen");
                Console.WriteLine("3. Favoriten anzeigen");
                Console.WriteLine("0. Zurück");
                Console.Write("Bitte wählen: ");
                string? auswahl = Console.ReadLine();

                switch (auswahl)
                {
                    case "1":
                        AnimeListeAnzeigen();
                        break;

                    case "2":
                        Console.Write("Bitte Anime-Nummer eingeben: ");
                        if (int.TryParse(Console.ReadLine(), out int nummer))
                        {
                            benutzerVerwalter.AnimeFavoritenHinzufügen(
                                nummer,
                                animeDatenVerwalter.AlleAnime);
                        }
                        else
                        {
                            Console.WriteLine("Ungültige Eingabe.");
                        }
                        Console.WriteLine("Weiter mit einer beliebigen Taste");
                        Console.ReadKey();
                        break;

                    case "3":
                        benutzerVerwalter.FavoritenAnzeigen(animeDatenVerwalter.AlleAnime);
                        Console.WriteLine("Weiter mit einer beliebigen Taste");
                        Console.ReadKey();
                        break;

                    case "0":
                        zurueck = true;
                        break;

                    default:
                        Console.WriteLine("Ungültige Auswahl.");
                        Console.WriteLine("Weiter mit einer beliebigen Taste");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AnimeListeAnzeigen()
        {
            Console.Clear();
            Console.WriteLine("=== Verfügbare Anime ===");

            foreach (var anime in animeDatenVerwalter.AlleAnime)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Nummer: {anime.AnimeNummer}");
                Console.WriteLine($"Titel: {anime.AnimeTitel}");
                Console.WriteLine($"Episoden: {anime.EpisodenAnzahl}");
                Console.WriteLine($"Bewertung: {anime.DurchschnittsBewertung}");
            }

            Console.WriteLine("Weiter mit einer beliebigen Taste");
            Console.ReadKey();
        }
    }
}


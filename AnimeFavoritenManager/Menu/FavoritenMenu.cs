using AnimeFavoritenManager.Daten;
using AnimeFavoritenManager.Modelle;
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

        public void FavoritenStarten()
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
                        AnimeFavoritenHinzufuegen();
                        break;

                    case "3":
                        FavoritenAnzeigen();
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

            var alleAnime = animeDatenVerwalter.GetAnimeListe();

            if (alleAnime == null || alleAnime.Count == 0)
            {
                Console.WriteLine("Es sind keine Anime geladen. Bitte zuerst Anime aus der API laden.");
            }
            else
            {
                for (int i = 0; i < alleAnime.Count; i++)
                {
                    var anime = alleAnime[i];
                    int listenNummer = i + 1;

                    Console.WriteLine("-----------------------------");
                    Console.WriteLine($"[{listenNummer}] Titel: {anime.AnimeTitel}");
                    Console.WriteLine($"Episoden: {anime.EpisodenAnzahl}");
                    Console.WriteLine($"Bewertung: {anime.DurchschnittsBewertung}");
                }
            }

            Console.WriteLine("Weiter mit einer beliebigen Taste");
            Console.ReadKey();
        }

        private void AnimeFavoritenHinzufuegen()
        {
            Console.Clear();
            Console.WriteLine("=== Anime zu Favoriten hinzufügen ===");

            var alleAnime = animeDatenVerwalter.GetAnimeListe();

            if (alleAnime == null || alleAnime.Count == 0)
            {
                Console.WriteLine("Es sind keine Anime geladen. Bitte zuerst Anime aus der API laden.");
                Console.WriteLine("Weiter mit einer beliebigen Taste");
                Console.ReadKey();
                return;
            }

            // Optional: Liste kurz anzeigen
            for (int i = 0; i < alleAnime.Count; i++)
            {
                var anime = alleAnime[i];
                int listenIndex = i + 1;
                Console.WriteLine($"[{listenIndex}] {anime.AnimeTitel}");
            }

            Console.Write("Bitte Listen-Nummer des Animes eingeben: ");

            // 👉 HIER wird listenNummer EINMAL deklariert
            if (!int.TryParse(Console.ReadLine(), out int listenNummer))
            {
                Console.WriteLine("Ungültige Eingabe. Bitte eine Zahl eingeben.");
                Console.ReadKey();
                return;
            }

            if (listenNummer < 1 || listenNummer > alleAnime.Count)
            {
                Console.WriteLine("Es gibt keinen Anime mit dieser Nummer in der Liste.");
                Console.ReadKey();
                return;
            }

            int index = listenNummer - 1;
            var ausgewaehlterAnime = alleAnime[index];

            benutzerVerwalter.AnimeFavoritenHinzufügen(ausgewaehlterAnime.AnimeNummer, alleAnime);

            Console.ReadKey();
        }

        private void FavoritenAnzeigen()
        {
            Console.Clear();

            var alleAnime = animeDatenVerwalter.GetAnimeListe();
            benutzerVerwalter.FavoritenAnzeigen(alleAnime);

            Console.WriteLine("Weiter mit einer beliebigen Taste");
            Console.ReadKey();
        }
    }
}

using AnimeFavoritenManager.Daten;
using AnimeFavoritenManager.HelferKlassen;
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
                FarbAusgabe.SchreibeTitel("=== Favoriten-Menü ===");
                Console.WriteLine("1. Anime-Liste anzeigen");
                Console.WriteLine("2. Anime zu Favoriten hinzufügen");
                Console.WriteLine("3. Anime aus Favoriten entfernen");
                Console.WriteLine("4. Favoriten anzeigen");
                Console.WriteLine("0. Zurück");
                Console.Write("Bitte wählen: ");
                int auswahl = EingabeHelfer.LeseAuswahlZwischen(0, 4, true);

                switch (auswahl)
                {
                    case 1:
                        AnimeListeAnzeigen();
                        break;

                    case 2:
                        AnimeFavoritenHinzufuegen();
                        break;

                    case 3:
                        FavoritenEntfernen();
                        break;

                    case 4:
                        FavoritenAnzeigen();
                        break;
                    case 0:
                        zurueck = true;
                        break;

                    default:
                        FarbAusgabe.SchreibeFehler("Ungültige Auswahl.");
                        Console.WriteLine("Weiter mit einer beliebigen Taste");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AnimeListeAnzeigen()
        {
            Console.Clear();
            FarbAusgabe.SchreibeTitel("=== Verfügbare Anime ===");

            List<Anime>? alleAnime = animeDatenVerwalter.GetAnimeListe();

            if (alleAnime == null || alleAnime.Count == 0)
            {
                FarbAusgabe.SchreibeFehler("Es sind keine Anime geladen. Bitte zuerst Anime aus der API laden.");
                Console.WriteLine("Weiter mit einer beliebigen Taste");
                Console.ReadKey();
                return;
            }
            
                for (int i = 0; i < alleAnime.Count; i++)
                {
                    var anime = alleAnime[i];

                    Console.WriteLine("-----------------------------");

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($"[{i + 1}] ");
                    Console.ResetColor();

                    Console.WriteLine(anime.AnimeTitel);
                    Console.WriteLine(" - Score: " + anime.DurchschnittsBewertung +
                                      " - Episoden: " + anime.EpisodenAnzahl);
                }
            

            Console.WriteLine("Weiter mit einer beliebigen Taste");
            Console.ReadKey();

        }

        private void AnimeFavoritenHinzufuegen()
        {
            Console.Clear();
            FarbAusgabe.SchreibeTitel("=== Anime zu Favoriten hinzufügen ===");

            var alleAnime = animeDatenVerwalter.GetAnimeListe();

            if (alleAnime == null || alleAnime.Count == 0)
            {
                FarbAusgabe.SchreibeHinweis("Es sind keine Anime geladen. Bitte zuerst Anime aus der API laden.");
                Console.WriteLine("Weiter mit einer beliebigen Taste");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            //  Liste kurz anzeigen
            for (int i = 0; i < alleAnime.Count; i++)
            {
                var anime = alleAnime[i];
                int listenIndex = i + 1;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"[{listenIndex}]");
                Console.ResetColor();
                Console.WriteLine($" {anime.AnimeTitel}");
            }

            FarbAusgabe.SchreibeHinweis("Bitte Listen-Nummer des Animes eingeben (0 = Abbrechen): ");

            int listenNummer = EingabeHelfer.LeseAuswahlZwischen(0, alleAnime.Count, true);

            if (listenNummer == 0)
            {
                FarbAusgabe.SchreibeHinweis("Vorgang abgebrochen.");
                Console.ReadKey();
                return;
            }

            int index = listenNummer - 1;
            Anime ausgewaehlterAnime = alleAnime[index];

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

        private void FavoritenEntfernen()
        {
            {
                Console.Clear();

                var alleAnime = animeDatenVerwalter.GetAnimeListe();
                benutzerVerwalter.FavoritenEntfernen(alleAnime);

                Console.WriteLine();
                Console.WriteLine("Weiter mit einer beliebigen Taste");
                Console.ReadKey();
            }
        }

    }
}
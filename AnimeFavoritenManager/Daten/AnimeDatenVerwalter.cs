using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeFavoritenManager.Modelle;

namespace AnimeFavoritenManager.Daten
{
    internal class AnimeDatenVerwalter
    {
        public List<Anime> AlleAnime { get; set; } = new();

        public void AnimeHinzufuegen(Anime anime)
        {
            // Prüfen, ob es die Nummer schon gibt (damit keine doppelten Einträge entstehen)
            foreach (var vorhandenerAnime in AlleAnime)
            {
                if (vorhandenerAnime.AnimeNummer == anime.AnimeNummer)
                {
                    Console.WriteLine($"Anime mit der Nummer {anime.AnimeNummer} existiert bereits in der Liste.");
                    return;
                }
            }

            AlleAnime.Add(anime);

        }

        public void DummyDatenLaden() 
        {
            // Falls schon etwas in der Liste ist, zuerst leeren
            AlleAnime.Clear();

            var anime1 = new Anime
            {
                AnimeNummer = 1,
                AnimeTitel = "Fullmetal Alchemist: Brotherhood",
                DurchschnittsBewertung = 9.2,
                EpisodenAnzahl = 64,
                AnimeBeschreibung = "Zwei Brüder suchen mit Hilfe der Alchemie nach dem Stein der Weisen."
            };

            var anime2 = new Anime
            {
                AnimeNummer = 2,
                AnimeTitel = "Attack on Titan",
                DurchschnittsBewertung = 9.0,
                EpisodenAnzahl = 75,
                AnimeBeschreibung = "Die Menschheit kämpft gegen riesige Titanen."
            };

            var anime3 = new Anime
            {
                AnimeNummer = 3,
                AnimeTitel = "Demon Slayer",
                DurchschnittsBewertung = 8.8,
                EpisodenAnzahl = 26,
                AnimeBeschreibung = "Ein Junge wird Dämonenjäger, um seine Schwester zu retten."
            };

            var anime4 = new Anime
            {
                AnimeNummer = 4,
                AnimeTitel = "Jujutsu Kaisen",
                DurchschnittsBewertung = 8.7,
                EpisodenAnzahl = 24,
                AnimeBeschreibung = "Flüche bedrohen die Menschen, Jujutsu-Zauberer kämpfen dagegen."
            };

            var anime5 = new Anime
            {
                AnimeNummer = 5,
                AnimeTitel = "My Hero Academia",
                DurchschnittsBewertung = 8.3,
                EpisodenAnzahl = 88,
                AnimeBeschreibung = "In einer Welt voller Superkräfte will ein Junge Held werden."
            };

            // Jetzt alle über die Methode hinzufügen
            AnimeHinzufuegen(anime1);
            AnimeHinzufuegen(anime2);
            AnimeHinzufuegen(anime3);
            AnimeHinzufuegen(anime4);
            AnimeHinzufuegen(anime5);
        }
    }
}
    


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AnimeFavoritenManager.Modelle;
using AnimeFavoritenManager.Modelle.API;

namespace AnimeFavoritenManager.Daten
{
    internal class AnimeDatenVerwalter
    {
        public List<Anime> DummyAnime { get; set; } = new();
        private readonly HttpClient httpClient;
        private List<Anime> animeListe { get; set; } = new();

        public AnimeDatenVerwalter()
        {
             httpClient = new HttpClient();
        }

        public void AnimeHinzufuegen(Anime anime)
        {
            // Prüfen, ob es die Nummer schon gibt (damit keine doppelten Einträge entstehen)
            foreach (var vorhandenerAnime in DummyAnime)
            {
                if (vorhandenerAnime.AnimeNummer == anime.AnimeNummer)
                {
                    Console.WriteLine($"Anime mit der Nummer {anime.AnimeNummer} existiert bereits in der Liste.");
                    return;
                }
            }

            DummyAnime.Add(anime);

        }

        public void DummyDatenLaden() 
        {
            // Falls schon etwas in der Liste ist, zuerst leeren
            DummyAnime.Clear();

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

        public async Task AnimeVonApiLadenAsync()
        {
            try
            {
                // 1. URL festlegen
                string url = "https://api.jikan.moe/v4/top/anime";
                // 2. HTTP-Request schicken (await)
                var response = await httpClient.GetAsync(url);
                // 3. Status prüfen
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Fehler beim abrufen der Anime-Daten. Statuscode: " + response.StatusCode);
                    return;
                }

                // 4. JSON lesen (await)
                string json = await response.Content.ReadAsStringAsync();

                // 5. JSON deserialisieren
                var apiAntwort = JsonSerializer.Deserialize<JikanAnimeResponse>(json);

                if (apiAntwort == null || apiAntwort.Data == null || apiAntwort.Data.Count == 0)
                {
                    Console.WriteLine("Es konnten kene Daten aus der API geladen werden.");
                    return;
                }

                // 6. interne Liste leeren und füllen

                foreach (var eintrag in apiAntwort.Data)
                {
                    var anime = new Anime
                    {
                        AnimeNummer = eintrag.MalId,
                        AnimeTitel = eintrag.Title ?? "unbekannter Titel",
                        EpisodenAnzahl = eintrag.Episodes ?? 0,
                        DurchschnittsBewertung = eintrag.Score ?? 0,
                        AnimeBeschreibung = string.Empty
                    };
                    animeListe.Add(anime);
                }
                Console.WriteLine($"Es wurden {animeListe.Count} Anime aus der API geladen.");
            }
            catch (Exception ex)
            {
                // 7. Fehler für den Benutzer ausgeben
                Console.WriteLine("Beim Abrufen ist ein Fehler  aufgetreten:");
                Console.WriteLine(ex.Message);
            }
        }

        public void AnimeListeAnzeigen()
        {
            if ( animeListe.Count == 0 )
            {
                Console.WriteLine("Es wurden noch keine Anime geladen. Bitte zuerst Anime-Daten laden.");
                return;
            }
            
                Console.WriteLine("=== Geladene Anime ===");

            for (int i = 0; i < animeListe.Count; i++)
            {
                var anime = animeListe[i];
                int listenNummer = i + 1;

                Console.WriteLine($"[{listenNummer}] {anime.AnimeTitel} - Score: {anime.DurchschnittsBewertung} - Episoden: {anime.EpisodenAnzahl}");
            }



        }

        public List<Anime> GetAnimeListe()
        {
            return animeListe;
        }
    }
}
    


using AnimeFavoritenManager.Modelle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AnimeFavoritenManager.Daten
{
    internal class BenutzerVerwalter
    {
        private const string BenutzerDateiPfad = DateiPfad.BenutzerDateiPfad;
        private List<Benutzer> benutzerListe = new();

        public Benutzer? AngemeldeterBenutzer { get; private set; }

        public bool IstBenutzerAngemeldet => AngemeldeterBenutzer != null;

        public BenutzerVerwalter()
        {
            BenutzerLaden();
        }

        private void BenutzerLaden()
        // lädt Liste aus Json Datei, bei Fehler wird eine leere Liste verwendet
        {
            if (!File.Exists(BenutzerDateiPfad))
            {
                benutzerListe = new List<Benutzer>();
                return;
            }

            string jsonDaten = File.ReadAllText(BenutzerDateiPfad);

            if (string.IsNullOrEmpty(jsonDaten))
            {
                benutzerListe = new List<Benutzer>();
                return;
            }

            try
            {
                List<Benutzer>? benutzer = JsonSerializer.Deserialize<List<Benutzer>>(jsonDaten);
                benutzerListe = benutzer ?? new List<Benutzer>();
            }
            catch
            {
                benutzerListe = new List<Benutzer>();
            }
        }

        private void BenutzerSpeichern()
        // speichert Liste in Json Datei
        {
            var optionen = new JsonSerializerOptions { WriteIndented = true };
            string jsonDaten = JsonSerializer.Serialize(benutzerListe, optionen);
            File.WriteAllText(BenutzerDateiPfad, jsonDaten);
        }

        public void BenutzerRegistrieren()
        {
            Console.Clear();
            Console.WriteLine("=== Benutzer registrieren ===");
            Console.WriteLine();
            Console.Write("Bitte Benutzernamen eingeben: ");
            string? benutzername = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(benutzername))
            {
                Console.WriteLine("Benutzername darf nicht leer sein!");
                Console.WriteLine("Drücke eine Taste um fortzufahren");
                Console.ReadKey();
                return;
            }

            // Prüfung ob Name schon existiert
            if (benutzerListe.Any(b => b.BenutzerName.Equals(benutzername, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Benutzer existiert bereits!");
                Console.WriteLine("Drücke eine Taste um fortzufahren");
                Console.ReadKey();
                return;
            }

            Console.Write("Bitte Passwort eingeben: ");
            string? passwort = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(passwort))
            {
                Console.WriteLine("Passwort darf nicht leer sein.");
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();
                return;
            }

            int neueBenutzerID = (benutzerListe.Count == 0)
                ? 1
                : benutzerListe.Max(b => b.BenutzerID) + 1;

            var neuerBenutzer = new Benutzer
            {
                BenutzerID = neueBenutzerID,
                BenutzerName = benutzername,
                PasswortKlartext = passwort
            };

            benutzerListe.Add(neuerBenutzer);
            BenutzerSpeichern();

            Console.WriteLine("Benutzer wurde registriert");
            Console.WriteLine($"Deine Benutzer ID: {neuerBenutzer.BenutzerID}");
            Console.ReadKey();
        }

        public void BenutzerLogin()
        {
            Console.Clear();
            Console.WriteLine("=== Benutzer Login ===");
            Console.Write("Benutzername: ");
            string? benutzername = Console.ReadLine();

            Console.Write("Passwort: ");
            string? passwort = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(benutzername) || string.IsNullOrWhiteSpace(passwort))
            {
                Console.WriteLine("Benutzername und Passwort dürfen nicht leer sein.");
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();
                return;
            }

            Benutzer? benutzer = benutzerListe.FirstOrDefault(
                b => b.BenutzerName.Equals(benutzername, StringComparison.OrdinalIgnoreCase)
                     && b.PasswortKlartext == passwort);

            if (benutzer == null)
            {
                Console.WriteLine("Login fehlgeschlagen. Benutzername oder Passwort falsch.");
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();
                return;
            }

            AngemeldeterBenutzer = benutzer;

            Console.WriteLine($"Willkommen, {benutzer.BenutzerName}");
            Console.WriteLine("Du bist angemeldet!");
            Console.WriteLine("Beliebige Taste, um fortzufahren");
            Console.ReadKey();
        }

        public void BenutzerLogout()
        {
            Console.Clear();

            if (AngemeldeterBenutzer == null)
            {
                Console.WriteLine("Es ist kein Benutzer angemeldet.");
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Benutzer '{AngemeldeterBenutzer.BenutzerName}' wurde ausgeloggt.");
            AngemeldeterBenutzer = null;

            Console.WriteLine("Du bist nun abgemeldet.");
            Console.WriteLine("Drücke eine Taste, um fortzufahren...");
            Console.ReadKey();
        }

        public void AnimeFavoritenHinzufügen(int animeNummer, List<Anime> alleAnime)
        {
            if (AngemeldeterBenutzer == null)
            {
                Console.WriteLine("Bitte Anmelden!");
                Console.ReadKey();
                return;
            }

            bool nummerExistiert = alleAnime.Any(anime => anime.AnimeNummer == animeNummer);

            if (!nummerExistiert)
            {
                Console.WriteLine("Es gibt keinen Anime mit dieser Nummer. Bitte eine gültige Nummer eingeben");
                return;
            }

            if (AngemeldeterBenutzer.FavoritenAnimeNummern.Contains(animeNummer))
            {
                Console.WriteLine("Dieser Anime befindet sich bereits in deiner Favoritenliste.");
                return;
            }

            AngemeldeterBenutzer.FavoritenAnimeNummern.Add(animeNummer);
            BenutzerSpeichern();
            Console.WriteLine("Anime wurde zur Favoritenliste hinzugefügt");
        }

        public void FavoritenAnzeigen(List<Anime> alleAnime)
        {
            if (AngemeldeterBenutzer == null)
            {
                Console.WriteLine("Bitte zuerst anmelden.");
                return;
            }

            var favoritenIds = AngemeldeterBenutzer.FavoritenAnimeNummern;

            if (favoritenIds == null || favoritenIds.Count == 0)
            {
                Console.WriteLine("Du hast noch keine Favoriten.");
                return;
            }

            if (alleAnime == null || alleAnime.Count == 0)
            {
                Console.WriteLine("Es sind aktuell keine Anime-Daten geladen. Bitte zuerst Anime aus der API laden.");
                return;
            }

            Console.WriteLine("=== Deine Favoriten ===");

            int listenNummer = 1;

            foreach (var favId in favoritenIds)
            {
                var anime = alleAnime.FirstOrDefault(a => a.AnimeNummer == favId);
                if (anime == null)
                {
                    continue;
                }

                Console.WriteLine($"[{listenNummer}] {anime.AnimeTitel} - Score: {anime.DurchschnittsBewertung} - Episoden: {anime.EpisodenAnzahl}");
                listenNummer++;
            }

            if (listenNummer == 1)
            {
                Console.WriteLine("Keine passenden Anime zu deinen Favoriten gefunden (evtl. API-Liste anders).");
            }
        }
    }
}

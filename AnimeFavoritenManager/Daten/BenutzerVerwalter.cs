using AnimeFavoritenManager.HelferKlassen;
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
            FarbAusgabe.SchreibeTitel("=== Benutzer registrieren ===");
            Console.WriteLine();
            Console.Write("Bitte Benutzernamen eingeben: ");
            string? benutzername = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(benutzername))
            {
                FarbAusgabe.SchreibeFehler("Benutzername darf nicht leer sein!");
                Console.WriteLine("Drücke eine Taste um fortzufahren");
                Console.ReadKey();
                return;
            }

            // Prüfung ob Name schon existiert
            if (benutzerListe.Any(b => b.BenutzerName.Equals(benutzername, StringComparison.OrdinalIgnoreCase)))
            {
                FarbAusgabe.SchreibeHinweis("Benutzer existiert bereits!");
                Console.WriteLine("Drücke eine Taste um fortzufahren");
                Console.ReadKey();
                return;
            }

            Console.Write("Bitte Passwort eingeben: ");
            string? passwort = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(passwort))
            {
                FarbAusgabe.SchreibeFehler("Passwort darf nicht leer sein.");
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

            FarbAusgabe.SchreibeErfolg("Benutzer wurde registriert");
            Console.WriteLine($"Deine Benutzer ID: {neuerBenutzer.BenutzerID}");
            Console.ReadKey();
        }

        public void BenutzerLogin()
        {
            Console.Clear();
            FarbAusgabe.SchreibeTitel("=== Benutzer Login ===");
            Console.Write("Benutzername: ");
            string? benutzername = Console.ReadLine();

            Console.Write("Passwort: ");
            string? passwort = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(benutzername) || string.IsNullOrWhiteSpace(passwort))
            {
                FarbAusgabe.SchreibeFehler("Benutzername und Passwort dürfen nicht leer sein.");
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();
                return;
            }

            Benutzer? benutzer = benutzerListe.FirstOrDefault(
                b => b.BenutzerName.Equals(benutzername, StringComparison.OrdinalIgnoreCase)
                     && b.PasswortKlartext == passwort);

            if (benutzer == null)
            {
                FarbAusgabe.SchreibeFehler("Login fehlgeschlagen. Benutzername oder Passwort falsch.");
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();
                return;
            }

            AngemeldeterBenutzer = benutzer;

            Console.WriteLine($"Willkommen, {benutzer.BenutzerName}");
            FarbAusgabe.SchreibeErfolg("\nDu bist angemeldet!");
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
                FarbAusgabe.SchreibeHinweis("Bitte Anmelden!");
                Console.ReadKey();
                return;
            }

            bool nummerExistiert = alleAnime.Any(anime => anime.AnimeNummer == animeNummer);

            if (!nummerExistiert)
            {
                FarbAusgabe.SchreibeHinweis("Es gibt keinen Anime mit dieser Nummer. Bitte eine gültige Nummer eingeben");
                return;
            }

            if (AngemeldeterBenutzer.FavoritenAnimeNummern.Contains(animeNummer))
            {
                FarbAusgabe.SchreibeHinweis("Dieser Anime befindet sich bereits in deiner Favoritenliste.");
                return;
            }

            AngemeldeterBenutzer.FavoritenAnimeNummern.Add(animeNummer);
            BenutzerSpeichern();
            FarbAusgabe.SchreibeErfolg("Anime wurde zur Favoritenliste hinzugefügt");
        }

        public void FavoritenAnzeigen(List<Anime> alleAnime)
        {
            {
                List<int> favoritenIds;

                if (!VersucheFavoritenUndAnimeZuLaden(alleAnime, out favoritenIds))
                {
                    return;
                }

                FarbAusgabe.SchreibeTitel("=== Deine Favoriten ===");

                int listenNummer = 1;

                foreach (int favId in favoritenIds)
                {
                    Anime? anime = alleAnime.FirstOrDefault(a => a.AnimeNummer == favId);
                    if (anime == null)
                    {
                        continue;
                    }

                    Console.WriteLine("[" + listenNummer + "] " + anime.AnimeTitel +
                                      " - Score: " + anime.DurchschnittsBewertung +
                                      " - Episoden: " + anime.EpisodenAnzahl);
                    listenNummer++;
                }

                if (listenNummer == 1)
                {
                    FarbAusgabe.SchreibeFehler("Keine passenden Anime zu deinen Favoriten gefunden (evtl. API-Liste anders).");
                }
            }
        }

        public void FavoritenEntfernen(List<Anime> alleAnime)
        {
            List<int> favoritenIDs;

            if (!VersucheFavoritenUndAnimeZuLaden(alleAnime, out favoritenIDs))
            {
                return;
            }

            FarbAusgabe.SchreibeTitel("=== Favoriten entfernen ===");

            List<int> anzeigeliste = new List<int>();
            int listenNummer = 1;

            foreach (int favId in favoritenIDs)
            {
                Anime? anime = alleAnime.FirstOrDefault(a => a.AnimeNummer == favId);
                if (anime == null)
                {
                    continue;
                }

                Console.WriteLine("[" + listenNummer + "] " + anime.AnimeTitel +
                                  " – Bewertung: " + anime.DurchschnittsBewertung +
                                  " – Episoden: " + anime.EpisodenAnzahl);

                anzeigeliste.Add(favId);
                listenNummer++;
            }

            if (anzeigeliste.Count == 0)
            {
                FarbAusgabe.SchreibeHinweis("Zu deinen Favoriten wurden keine passenden Anime in der aktuellen Liste gefunden.");
                return;
            }

            Console.WriteLine();
            Console.Write("Bitte die Nummer des Favoriten eingeben,\ndie entfernt werden soll (oder 0 zum Abbrechen)\nEingabe: ");

            int auswahl = EingabeHelfer.LeseAuswahlZwischen(0, anzeigeliste.Count, true);

            if (auswahl == 0)
            {
                Console.WriteLine("Vorgang abgebrochen.");
                return;
            }


            int animeNummerZumLoeschen = anzeigeliste[auswahl - 1];

            bool entfernt = AngemeldeterBenutzer.FavoritenAnimeNummern.Remove(animeNummerZumLoeschen);

            if (entfernt)
            {
                BenutzerSpeichern();
                FarbAusgabe.SchreibeErfolg("Der ausgewählte Anime wurde aus deinen Favoriten entfernt.");
            }
            else
            {
                FarbAusgabe.SchreibeFehler("Der ausgewählte Anime konnte nicht aus der Favoritenliste entfernt werden.");
            }
        }

        private bool VersucheFavoritenUndAnimeZuLaden(List<Anime> alleAnime,out List<int> favoritenIds)
        {
            favoritenIds = new List<int>();

            if (AngemeldeterBenutzer == null)
            {
                FarbAusgabe.SchreibeHinweis("Bitte zuerst anmelden, um Favoriten zu verwalten.");
                return false;
            }

            if (AngemeldeterBenutzer.FavoritenAnimeNummern == null ||
                AngemeldeterBenutzer.FavoritenAnimeNummern.Count == 0)
            {
                FarbAusgabe.SchreibeHinweis("Du hast aktuell keine Favoriten.");
                return false;
            }

            if (alleAnime == null || alleAnime.Count == 0)
            {
                FarbAusgabe.SchreibeFehler("Es sind keine Anime-Daten geladen. Bitte zuerst Daten laden.");
                return false;
            }

            favoritenIds = AngemeldeterBenutzer.FavoritenAnimeNummern;
            return true;
        }

    }
}

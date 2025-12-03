using AnimeFavoritenManager.Modelle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnimeFavoritenManager.Daten
{
    internal class BenutzerVerwalter
    {
        private const string BenutzerDateiPfad = "benutzer.json";
        private List<Modelle.Benutzer> benutzerListe = new();

        public Modelle.Benutzer? AngemeldeterBenutzer { get; private set; }


        public BenutzerVerwalter()
        {
            BenutzerLaden();
        }

        private void BenutzerLaden()
        {
            if (!File.Exists(BenutzerDateiPfad))
            {
                benutzerListe = new List<Modelle.Benutzer>();
                return;
            }

            string jsonDaten = File.ReadAllText(BenutzerDateiPfad);

            if (string.IsNullOrEmpty(jsonDaten))
            {
                benutzerListe = new List<Modelle.Benutzer>();
                return;
            }

            try
            {
                List<Modelle.Benutzer> benutzer = JsonSerializer.Deserialize<List<Modelle.Benutzer>>(jsonDaten);
                benutzerListe = benutzer ?? new List<Modelle.Benutzer>();
            }
            catch
            {
                benutzerListe = new List<Modelle.Benutzer>();
            }

        }
        private void BenutzerSpeichern()
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

            // Prüfung ob name schon existiert
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
                ? 1 : benutzerListe.Max(b => b.BenutzerID) + 1;

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

            Modelle.Benutzer benutzer = benutzerListe
                .FirstOrDefault(b => b.BenutzerName.Equals(benutzername, StringComparison.OrdinalIgnoreCase) && b.PasswortKlartext == passwort);

            if (benutzer == null)
            {
                Console.WriteLine("Login fehlgeschlagen. Benutzername oder Passwort falsch.");
                Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                Console.ReadKey();
                return;
            }

            AngemeldeterBenutzer = benutzer;

            Console.WriteLine($"Willkommen, {benutzer.BenutzerName}");
            Console.WriteLine(" Du bist angemeldet!");
            Console.WriteLine("beliebige Taste um fortzufahren");
            Console.ReadKey();
        }
    }
    
}


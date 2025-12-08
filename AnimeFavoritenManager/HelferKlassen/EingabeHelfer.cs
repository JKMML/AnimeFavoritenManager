using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFavoritenManager.HelferKlassen
{
    internal class EingabeHelfer
    {
        public static int LeseAuswahlZwischen(int min, int max, bool darfNullSein)
        {
            while (true)
            {
                string? eingabe = Console.ReadLine();
                int zahl;

                if (!int.TryParse(eingabe, out zahl))
                {
                    FarbAusgabe.SchreibeFehler("Ungültige Eingabe. Bitte eine Zahl eingeben.");
                    Console.Write("Eingabe: ");
                    continue;
                }

                if (darfNullSein && zahl == 0)
                {
                    return 0;
                }

                if (zahl < min || zahl > max)
                {
                    FarbAusgabe.SchreibeFehler("Ungültige Auswahl. Bitte eine Zahl zwischen " + min + " und " + max + " eingeben.");
                    Console.Write("Eingabe: ");
                    continue;
                }

                return zahl;
            }
        }
    }
}

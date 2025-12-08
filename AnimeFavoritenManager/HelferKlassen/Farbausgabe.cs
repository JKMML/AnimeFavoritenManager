using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFavoritenManager.HelferKlassen
{
        internal static class FarbAusgabe
        {
            public static void SchreibeTitel(string text)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(text);
                Console.ResetColor();
            }

            public static void SchreibeHinweis(string text)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(text);
                Console.ResetColor();
            }

            public static void SchreibeFehler(string text)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(text);
                Console.ResetColor();
            }

            public static void SchreibeErfolg(string text)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(text);
                Console.ResetColor();
            }
        }
    }


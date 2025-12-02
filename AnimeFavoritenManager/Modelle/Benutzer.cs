using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFavoritenManager.Modelle
{
    internal class Benutzer
    {
        public int BenutzerID { get; set; }
        public string BenutzerName { get; set; } = string.Empty;
        public string PasswortKlartext { get; set; } = string.Empty;

        // Speichern von Anime nummern für favoriten liste
        public List<int> FavoritenAnimeNummern { get; set; } = new();
    }
}

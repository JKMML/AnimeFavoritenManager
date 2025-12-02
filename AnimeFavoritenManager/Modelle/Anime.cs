using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFavoritenManager.Modelle
{
    internal class Anime
    {
        public int AnimeNummer { get; set; }
        public string AnimeTitel { get; set; } = string.Empty;
        public double DurchschnittsBewertung { get; set; }
        public int EpisodenAnzahl { get; set; }
        public string AnimeBeschreibung { get; set; } = string.Empty;

    }
}

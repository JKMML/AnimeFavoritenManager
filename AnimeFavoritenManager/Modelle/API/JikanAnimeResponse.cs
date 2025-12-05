using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace AnimeFavoritenManager.Modelle.API
{
    internal class JikanAnimeResponse
    {
        [JsonPropertyName("data")]
        public List<JikanAnime> Data { get; set; } = new();
    }

    public class JikanAnime
    {
        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("episodes")]
        public int? Episodes { get; set; }

        [JsonPropertyName("score")]
        public double? Score { get; set; }
    }
}

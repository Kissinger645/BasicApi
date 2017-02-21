using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BasicApi.Models
{
    public class Game
    {
        public List<object> abilities { get; set; }
        public string name { get; set; }
        public List<VersionGroup> version_groups { get; set; }
        public int id { get; set; }
        public List<Name> names { get; set; }
        public List<PokemonSpecy> pokemon_species { get; set; }
        public List<Move> moves { get; set; }
        public MainRegion main_region { get; set; }
        public List<Type> types { get; set; }
    }

    

    public class Language
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Name
    {
        public string name { get; set; }
        public Language language { get; set; }
    }

    public class PokemonSpecy
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class MainRegion
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class GameCollection
    {
        public string Count { get; set; }
        public Uri Next { get; set; }
        public Uri Previous { get; set; }
        public List<Game> Results { get; set; }

        public GameCollection GetPage(HttpClient client, Uri page)
        {
            if (page != null)
            {
                string pageNumber = page.Query;
                var allPokemonResponse = client.GetAsync($"generation{pageNumber}").Result;
                return allPokemonResponse.Content.ReadAsAsync<GameCollection>().Result;
            }
            return this;
        }

        public GameCollection GetPrevious(HttpClient client)
        {
            return GetPage(client, Previous);
        }

        public GameCollection GetNext(HttpClient client)
        {
            return GetPage(client, Next);
        }
    }
}

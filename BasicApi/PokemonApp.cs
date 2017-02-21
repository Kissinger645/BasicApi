using BasicApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BasicApi
{
    class PokemonApp
    {
        public static HttpClient client = new HttpClient();

        static string Read(string input)
        {
            Console.WriteLine(input);
            return Console.ReadLine();
        }

        public static void PokeApp()
        {
            bool runAgain = true;
            while (runAgain)
            {
                Console.WriteLine("Press 1 to see a list of all Pokemon");
                Console.WriteLine("Press 2 to see details of a Pokemon");
                Console.WriteLine("Press 3 to see a list of all Games");
                Console.WriteLine("Press 4 to see details of a Game");
                Console.WriteLine("Press 5 to see a list of all Items");
                Console.WriteLine("Press 6 to see details of an Item");
                Console.WriteLine("Press 9 to exit");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        GetPokemonList();
                        break;
                    case "2":
                        GetPokemon();
                        break;
                    case "3":
                        GetGameList();
                        break;
                    case "4":
                        GetGame();
                        break;
                    case "5":
                        GetItemList();
                        break;
                    case "6":
                        GetItem();
                        break;
                    default:
                        runAgain = false;
                        break;
                }
            }
        }

        public static void SetUpClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("http://pokeapi.co/api/v2/");
        }

        public static GameList()
        {
            var allGameResponse = client.GetAsync("game").Result;
            GameCollection allPokemon = allGameResponse.Content.ReadAsAsync<GameCollection>().Result;
            
        }

        public static ItemList()
        {
            var allGameResponse = client.GetAsync("item").Result;
            ItemCollection allPokemon = allGameResponse.Content.ReadAsAsync<ItemCollection>().Result;

        }

        public static PokemonList()
        {
            var allPokemonResponse = client.GetAsync("pokemon").Result;
            PokemonCollection allPokemon = allPokemonResponse.Content.ReadAsAsync<PokemonCollection>().Result;

        }

        static Pokemon GetPokemon(string id)
        {
            var response = client.GetAsync($"pokemon/{id}/").Result;
            return response.Content.ReadAsAsync<Pokemon>().Result;
        }

        static Game GetGame(string id)
        {
            var response = client.GetAsync($"game/{id}/").Result;
            return response.Content.ReadAsAsync<Game>().Result;
        }

        static Item GetItem(string id)
        {
            var response = client.GetAsync($"item/{id}/").Result;
            return response.Content.ReadAsAsync<Item>().Result;
        }


    }
}

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
    public class PokemonApp
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
                        Console.Clear();
                        PokemonList();
                        break;

                    case "2":
                        Console.Clear();
                        var thisPoke = GetPokemon(Read("Enter name or Id of Pokemon"));
                        Console.WriteLine(thisPoke.name);
                        Console.WriteLine(thisPoke.height);
                        Console.WriteLine(thisPoke.weight);
                        Console.WriteLine(thisPoke.moves);
                        Console.WriteLine("Press any key to return to the menu");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        GameList();
                        break;

                    case "4":
                        Console.Clear();
                        GetGame(Read("Enter name or Id of Game"));
                        break;

                    case "5":
                        Console.Clear();
                        ItemList();
                        break;

                    case "6":
                        Console.Clear();
                        GetItem(Read("Enter name or Id of Item"));
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

        public static void GameList()
        {
            var allGameResponse = client.GetAsync("generation").Result;
            GameCollection allGames = allGameResponse.Content.ReadAsAsync<GameCollection>().Result;
        }

        public static void ItemList()
        {
            var allGameResponse = client.GetAsync("item").Result;
            ItemCollection allItems = allGameResponse.Content.ReadAsAsync<ItemCollection>().Result;
        }

        public static void PokemonList()
        {
            var allPokemonResponse = client.GetAsync("pokemon").Result;
            PokemonCollection allPokemon = allPokemonResponse.Content.ReadAsAsync<PokemonCollection>().Result;
        }

        public static  Pokemon GetPokemon(string id)
        {
            var response = client.GetAsync($"pokemon/{id}/").Result;
            return response.Content.ReadAsAsync<Pokemon>().Result;

        }

        public static Game GetGame(string id)
        {
            var response = client.GetAsync($"generation/{id}/").Result;
            return response.Content.ReadAsAsync<Game>().Result;
        }

        static Item GetItem(string id)
        {
            var response = client.GetAsync($"item/{id}/").Result;
            return response.Content.ReadAsAsync<Item>().Result;
        }
    }
}

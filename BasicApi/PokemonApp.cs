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
                        var allPokemonResponse = client.GetAsync("pokemon").Result;
                        PokemonCollection allPokemon = allPokemonResponse.Content.ReadAsAsync<PokemonCollection>().Result;

                        bool question = true;
                        while (question == true)
                        {
                            foreach (var pokemon in allPokemon.Results)
                            {
                                Console.WriteLine(pokemon.name);
                            }

                            var answer = Read("Press 9 for the next page, 1 for the previous page or 0 to return to the menu");
                            switch (answer)
                            {
                                case "1":
                                    allPokemon = allPokemon.GetPrevious(client);
                                    break;

                                case "9":
                                    allPokemon = allPokemon.GetNext(client);
                                    break;

                                default:
                                    question = false;
                                    break;
                            }
                        }
                        Console.WriteLine("Press any key to return to the menu");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        var thisPoke = GetPokemon(Read("Enter name or Id of Pokemon"));
                        Console.WriteLine();
                        Console.WriteLine(thisPoke.name);
                        Console.WriteLine(thisPoke.height);
                        Console.WriteLine(thisPoke.weight);
                        foreach (var move in thisPoke.moves)
                        {
                            Console.WriteLine($"{move.move.name}");
                        }
                        Console.WriteLine("Press any key to return to the menu");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        var allGameResponse = client.GetAsync("generation").Result;
                        PokemonCollection allGame = allGameResponse.Content.ReadAsAsync<PokemonCollection>().Result;
                        foreach (var game in allGame.Results)
                        {
                            Console.WriteLine(game.name);
                        }

                        Console.WriteLine("Press any key to return to the menu");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Clear();
                        var thisGame = GetGame(Read("Enter name or Id of Game"));
                        Console.WriteLine(thisGame.name);
                        foreach (var name in thisGame.names)
                        {
                            Console.WriteLine($"{name.language.name}");
                        }
                        Console.WriteLine(thisGame.version_groups);
                        Console.WriteLine("Press any key to return to the menu");
                        Console.ReadKey();
                        break;

                    case "5":
                        Console.Clear();
                        var allItemResponse = client.GetAsync("item").Result;
                        PokemonCollection allItem = allItemResponse.Content.ReadAsAsync<PokemonCollection>().Result;
                        foreach (var item in allItem.Results)
                        {
                            Console.WriteLine(item.name);
                        }
                        Console.WriteLine("Press any key to return to the menu");
                        Console.ReadKey();
                        break;

                    case "6":
                        Console.Clear();
                        var thisItem = GetItem(Read("Enter name or Id of Item"));
                        Console.WriteLine(thisItem.Name);
                        Console.WriteLine(thisItem.AttributeName);
                        Console.WriteLine("Press any key to return to the menu");
                        Console.ReadKey();
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

    public static Pokemon GetPokemon(string id)
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

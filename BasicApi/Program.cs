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
    class Program
    {
        static void Main(string[] args)
        {
            PokemonApp.SetUpClient();
            PokemonApp.PokeApp();
        }
    }
}

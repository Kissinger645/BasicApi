using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BasicApi.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Cost { get; set; }
        public string AttributeName { get; set; }
    }

    public class ItemCollection
    {
        public string Count { get; set; }
        public Uri Next { get; set; }
        public Uri Previous { get; set; }
        public List<Item> Results { get; set; }


        private ItemCollection GetPage(HttpClient client, Uri page)
        {
            if (page != null)
            {
                string pageNumber = page.Query;
                var allPokemonResponse = client.GetAsync($"item{pageNumber}").Result;
                return allPokemonResponse.Content.ReadAsAsync<ItemCollection>().Result;
            }
            return this;
        }

        public ItemCollection GetPrevious(HttpClient client)
        {
            return GetPage(client, Previous);
        }

        public ItemCollection GetNext(HttpClient client)
        {
            return GetPage(client, Next);
        }
    }
}

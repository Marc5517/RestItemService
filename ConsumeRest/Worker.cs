using ModelLib.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeRest
{
    internal class Worker
    {
        private const string URI = "https://restitemservice.azurewebsites.net/api/localItems";

        public Worker()
        {

        }

        public async void Start()
        {
            PrintHeader("Henter alle Items");
            IList<Item> allItems = await GetAllItemsAsync();
            foreach (Item item in allItems)
            {
                Console.WriteLine(item);
            }


            PrintHeader("Henter Item med id nummer 2");
            try
            {
                Item item1 = await GetOneItemsAsync(2);
                Console.WriteLine("item= " + item1);
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }

            PrintHeader("Henter Item med id nummer 5");
            try
            {
                Item item1 = await GetOneItemsAsync(5);
                Console.WriteLine("item= " + item1);
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }


            PrintHeader("Opretter en Item");
            Item nyItem = new Item(7, "Chocolade", "High", 8.5);
            await OpretNyItem(nyItem);

            // udskriver alle Items
            allItems = await GetAllItemsAsync();
            foreach (Item item in allItems)
            {
                Console.WriteLine(item);
            }


            PrintHeader("Ændre Item id nummer 7");
            nyItem.Quality = "Really High";
            await OpdatereItem(nyItem);

            // udskriver alle Items
            allItems = await GetAllItemsAsync();
            foreach (Item item in allItems)
            {
                Console.WriteLine(item);
            }


            PrintHeader("Sletter nr 7");
            await SletItem(7);

            // udskriver alle Items
            allItems = await GetAllItemsAsync();
            foreach (Item item in allItems)
            {
                Console.WriteLine(item);
            }

            //Console.ReadLine();
        }

        private void PrintHeader(string txt)
        {
            Console.WriteLine("=========================");
            Console.WriteLine(txt);
            Console.WriteLine("=========================");

        }


        public async Task<IList<Item>> GetAllItemsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI);
                IList<Item> cList = JsonConvert.DeserializeObject<IList<Item>>(content);
                return cList;
            }
        }

        public async Task<Item> GetOneItemsAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(URI + id);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    Item item = JsonConvert.DeserializeObject<Item>(json);
                    return item;
                }
                // Else
                throw new KeyNotFoundException(
                    $"Fejl code={resp.StatusCode} message={await resp.Content.ReadAsStringAsync()}");
            }

        }

        public async Task OpretNyItem(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(item),
                    Encoding.UTF8,
                    "application/json");

                HttpResponseMessage resp = await client.PostAsync(URI, content);
                if (resp.IsSuccessStatusCode)
                {
                    return;
                }
                // Else
                throw new ArgumentException("Opret fejlede");
            }
        }

        public async Task OpdatereItem(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(item),
                    Encoding.UTF8,
                    "application/json");

                HttpResponseMessage resp = await client.PutAsync(URI + item.Id, content);
                if (resp.IsSuccessStatusCode)
                {
                    return;
                }

                throw new ArgumentException("Opdatering fejlede");
            }
        }

        public async Task SletItem(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.DeleteAsync(URI + id);
                if (resp.IsSuccessStatusCode)
                {
                    return;
                }
                // Else
                throw new ArgumentException("Slet fejlede");
            }
        }
    }
}

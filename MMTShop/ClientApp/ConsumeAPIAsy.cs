using MMTShop.Data.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ClientApp
{
    class ConsumeAPIAsy

    {
        string baseUrl = "https://localhost:44309/api/";
        public void GetAllFeaturedProducts()  
        {
           using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                //HTTP GET
                var responseTask = client.GetAsync("Product/Featured");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product[]>();
                    readTask.Wait();
                    var products = readTask.Result;
                    foreach (var product in products)
                    {
                        Console.WriteLine($"{ product.Id} -  { product.Name} : { product.Price} GBP");
                    }
                }
            }
        }

        public void GetAllCategories()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                //HTTP GET
                var responseTask = client.GetAsync("Category/Active");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category[]>();
                    readTask.Wait();
                    var categories = readTask.Result;
                    foreach (var category in categories)
                    {
                        Console.WriteLine($"{ category.Id} -  { category.Name}");
                    }
                }
            }
        }

        public void GetProducts(int categoryId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                //HTTP GET
                var responseTask = client.GetAsync($"Product/{categoryId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product[]>();
                    readTask.Wait();
                    var products = readTask.Result;
                    foreach (var product in products)
                    {
                        Console.WriteLine($"{ product.Id} -  { product.Name} : { product.Price} GBP");
                    }
                }
            }
        }
    }
}

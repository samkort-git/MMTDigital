using System;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsumeAPIAsy api = new ConsumeAPIAsy();
            Console.WriteLine("Welcome to MMT Shop");
           
            Console.WriteLine("=====Our Featured Products=====");
            api.GetAllFeaturedProducts();
           
            Console.WriteLine("====Products Categories====");
            api.GetAllCategories();
           
            Console.WriteLine("Enter Category Code:");
            int catId = Convert.ToInt32(Console.ReadLine());
            api.GetProducts(catId);


           Console.Read();
        }
    }
}

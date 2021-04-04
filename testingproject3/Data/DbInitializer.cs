using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testingproject3.Models;


namespace testingproject3.Data
{
    public class DbInitializer
    {
        public static void Initialize(ProductContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var products = new Product[]
                {
                    new Product{ ProductID=1001, Description="Testing1", Price = 1.1m, ImageUrl = "https://user-images.githubusercontent.com/41929050/61567048-13938600-aa33-11e9-9cfd-712191013192.jpeg" },
                    new Product{ ProductID=1002, Description="Testing2", Price = 2.1m, ImageUrl = "https://user-images.githubusercontent.com/41929050/61567049-13938600-aa33-11e9-9c69-a4184bf8e524.jpeg" },
                    new Product{ ProductID=1003, Description="Testing3", Price = 3.1m, ImageUrl = "https://user-images.githubusercontent.com/41929050/61567053-13938600-aa33-11e9-9780-104fe4019659.png" }
                };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();
        }
    }
}

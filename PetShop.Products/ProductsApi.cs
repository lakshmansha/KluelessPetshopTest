using System.Collections.Generic;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PetShop.Model;
using PetShop.Business;

namespace PetShop.Products
{
    public static class Products
    {
        [FunctionName("GetProducts")]
        public static IEnumerable<Product> GetProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            // string requestBody = new StreamReader(req.Body).ReadToEnd();
            // log.LogInformation("Input:" + requestBody);
            // Product products = JsonConvert.DeserializeObject<Product>(requestBody);

            var productService = new ProductService();
            return productService.GetData();
        }

        [FunctionName("SaveProducts")]
        public static bool SaveProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            log.LogInformation("Input:" + requestBody);
            Product product = JsonConvert.DeserializeObject<Product>(requestBody);

            var productService = new ProductService();
            return productService.AddProduct(product);
        }
    }
}

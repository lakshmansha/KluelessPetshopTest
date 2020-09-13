using System.Collections.Generic;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PetShop.Model;
using PetShop.Business;
using Microsoft.Extensions.Configuration;

namespace PetShop.Products
{
    public class Products
    {
        private IProductService _productService;
        private IConfiguration _configuration;
        public Products(IProductService productService, IConfiguration configuration)
        {
            _configuration = configuration;
            _productService = productService;
        }

        [FunctionName("GetProducts")]
        public IEnumerable<Product> GetProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return _productService.GetData();
        }

        [FunctionName("SaveProducts")]
        public bool SaveProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            log.LogInformation("Input:" + requestBody);
            Product product = JsonConvert.DeserializeObject<Product>(requestBody);
            
            return _productService.AddProduct(product);
        }
    }
}

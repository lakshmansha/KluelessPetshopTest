using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PetShop.Business;

[assembly: FunctionsStartup(typeof(PetShop.Products.Startup))]
namespace PetShop.Products
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {    
            // Registering services
            builder
                .Services
                .AddSingleton<IProductService, ProductService>();
        }
    }
}

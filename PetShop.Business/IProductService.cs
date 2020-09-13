using System.Collections.Generic;
using PetShop.Model;

namespace PetShop.Business
{
    public interface IProductService
    {
        bool AddProduct(Product product);
        IList<Product> GetData();
    }
}
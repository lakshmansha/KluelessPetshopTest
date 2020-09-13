#region Copyright PETShop
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
#endregion

using System;
using System.Collections.Generic;

using System.Linq;
using Microsoft.Extensions.Configuration;
using PetShop.DBAccess;
using Product = PetShop.Model.Product;

namespace PetShop.Business
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IList<Product> GetData()
        {
            using (var context = new PetShopContext(this._configuration))
            {
                var query = from product in context.Products
                            select
                                new Model.Product
                                {
                                    ProductId = product.ProductId,
                                    ProductName = product.ProductName,
                                    AvailableQuantity = product.Quantity
                                };
                return query.ToList();
            }
        }

        public bool AddProduct(Product product)
        {
            var dbProduct = new DBAccess.DataModel.Product
            {

                ProductId = product.ProductId == Guid.Empty ? Guid.NewGuid() : product.ProductId,
                ProductName = product.ProductName,
                Quantity = product.AvailableQuantity
            };

            using (var context = new PetShopContext(this._configuration))
            {
                context.Products.Add(dbProduct);
                context.SaveChanges();
            }

            return true;
        }

    }
}
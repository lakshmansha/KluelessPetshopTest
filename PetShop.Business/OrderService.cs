﻿#region Copyright PETShop
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
#endregion

using System;
using Microsoft.Extensions.Configuration;
using PetShop.DBAccess;
using Order = PetShop.DBAccess.DataModel;

namespace PetShop.Business
{
    public class OrderService : IOrderService
    {

        private readonly IConfiguration _configuration;

        public OrderService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public bool SaveData(Model.Order order)
        {
            var orderData = new Order.Order
            {
                OrderId = Guid.NewGuid(),
                ProductId = order.ProductId,
                CustomerId = PetShopDbConfiguration.DefaultCustomerID,
                Quantity = order.Quantity
            };

            using (var context = new PetShopContext(this._configuration))
            {
                context.Orders.Add(orderData);
                var product = context.Products.Find(order.ProductId);
                if (order.Quantity > product.Quantity)
                {
                    throw new DataMisalignedException("quantity more than available");
                }
                product.Quantity -= order.Quantity;
                context.SaveChanges();
            }

            return true;
        }
    }
}
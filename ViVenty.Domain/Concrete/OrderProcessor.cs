using System.Text;
using ViVenty.Domain.Entities;
using ViVenty.Domain.Abstract;
using System.Net;
using System.Net.Mail;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ViVenty.Domain.Concrete
{

    public class OrderProcessor : IOrderProcessor
    {
        private IViventyRepository repo;
        public Order order { get; set; }

        public OrderProcessor (IViventyRepository repo)
        {
            this.repo = repo;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            order = new Order();
            order.Name = shippingDetails.Name;
            order.Phone = shippingDetails.Phone;
            order.Email = shippingDetails.Email;
            order.Address = shippingDetails.Address;
            order.TimeStamp = DateTime.Now;
            order.Amount = cart.ComputeTotalValue();
            
            repo.AddOrder(order);           
            
        }
    }
}

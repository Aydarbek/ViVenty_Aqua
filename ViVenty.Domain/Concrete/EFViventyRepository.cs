using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViVenty.Domain.Entities;
using ViVenty.Domain.Abstract;

namespace ViVenty.Domain.Concrete
{
    public class EFViventyRepository : IViventyRepository
    {
        ViventyContext context = new ViventyContext();
        public IEnumerable<Hsuit> Hsuits
        {            
            get
            {
                return context.Hsuits;
            }
        }

        public IEnumerable<Photo> Photos
        {
            get
            {
                return context.Photos;
            }
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                return context.Orders;
            }           
        }

        public void AddOrder (Order order)
        {
            if (order != null)
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }
    }
}

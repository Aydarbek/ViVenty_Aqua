using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViVenty.Domain.Entities;

namespace ViVenty.Domain.Abstract
{
    public interface IEmailService
    {
        void SendOrderDetailsToAdmin(Cart cart, Order order);
        void SendOrderDetailsToClient(Cart cart, Order order);
    }
}

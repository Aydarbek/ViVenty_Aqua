using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViVenty.Domain.Entities;

namespace ViVenty.Domain.Abstract
{
    public interface IViventyRepository
    {
        IEnumerable<Hsuit> Hsuits { get; }
        IEnumerable<Photo> Photos { get; }
        IEnumerable<Order> Orders { get; }
        void AddOrder(Order order);

    }
}

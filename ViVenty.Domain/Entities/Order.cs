using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViVenty.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Amount { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}

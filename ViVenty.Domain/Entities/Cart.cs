using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViVenty.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Hsuit hsuit, int quantity)
        {
            CartLine line = lineCollection.
                Where(h => h.Hsuit.Id == hsuit.Id).
                FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Hsuit = hsuit,
                    Quantity = quantity
                });
            }
            else
                line.Quantity += quantity;
        }

        public void RemoveLine(Hsuit hsuit)
        {
            lineCollection.RemoveAll(l => l.Hsuit.Id == hsuit.Id);
        }

        public int ComputeTotalValue()
        {
            return lineCollection.Sum(l => l.Hsuit.Price * l.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Hsuit Hsuit { get; set; }
        public int Quantity { get; set; }
    }

}

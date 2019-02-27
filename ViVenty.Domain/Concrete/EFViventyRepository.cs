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
        DBContext context = new DBContext();
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
    }
}

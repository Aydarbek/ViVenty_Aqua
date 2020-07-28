namespace ViVenty.Domain.Concrete
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ViVenty.Domain.Entities;


    public class ViventyContext : DbContext
    {
        static ViventyContext()
        {
            Database.SetInitializer<ViventyContext>(new DbInitializer());
        }

        public virtual DbSet<Hsuit> Hsuits { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

    }
}
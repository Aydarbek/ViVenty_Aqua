namespace ViVenty.Domain.Concrete
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ViVenty.Domain.Entities;


    public class DBContext : DbContext
    {
        // Your context has been configured to use a 'Context' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ViVenty.Domain.Entities.Context' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Context' 
        // connection string in the application configuration file.


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Hsuit> Hsuits { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
    }
}
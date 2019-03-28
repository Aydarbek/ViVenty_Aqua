namespace ViVenty.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ViventyDB_v31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "Nr", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "Nr");
        }
    }
}

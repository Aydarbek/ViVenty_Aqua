namespace ViVenty.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ViventyDB_v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "Image", c => c.String());
            DropColumn("dbo.Photos", "Path");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "Path", c => c.String());
            DropColumn("dbo.Photos", "Image");
        }
    }
}

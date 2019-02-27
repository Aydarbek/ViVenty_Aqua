namespace ViVenty.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Viventy_DB_v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hsuits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Size = c.String(),
                        Price = c.Int(nullable: false),
                        DefaultPhoto = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        hsuit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hsuits", t => t.hsuit_Id)
                .Index(t => t.hsuit_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "hsuit_Id", "dbo.Hsuits");
            DropForeignKey("dbo.Hsuits", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Photos", new[] { "hsuit_Id" });
            DropIndex("dbo.Hsuits", new[] { "Category_Id" });
            DropTable("dbo.Photos");
            DropTable("dbo.Hsuits");
            DropTable("dbo.Categories");
        }
    }
}

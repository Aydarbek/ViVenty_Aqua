namespace ViVenty.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ViventyDB_v2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Hsuits", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Hsuits", new[] { "Category_Id" });
            AddColumn("dbo.Hsuits", "Category", c => c.String());
            DropColumn("dbo.Hsuits", "Category_Id");
            DropTable("dbo.Categories");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.Hsuits", "Category_Id", c => c.Int());
            DropColumn("dbo.Hsuits", "Category");
            CreateIndex("dbo.Hsuits", "Category_Id");
            AddForeignKey("dbo.Hsuits", "Category_Id", "dbo.Categories", "Id");
        }
    }
}

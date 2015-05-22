namespace Auction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Lots", "ImageData", c => c.Binary());
            AddColumn("dbo.Lots", "ImageMimeType", c => c.String());
            AddColumn("dbo.Lots", "Category_CategoryId", c => c.Int());
            AlterColumn("dbo.Lots", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Lots", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Lots", "IsCompleted", c => c.Boolean());
            AlterColumn("dbo.Lots", "CurrentPrice", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.Lots", "Category_CategoryId");
            AddForeignKey("dbo.Lots", "Category_CategoryId", "dbo.Categories", "CategoryId");
            DropColumn("dbo.Lots", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "Category", c => c.String());
            DropForeignKey("dbo.Lots", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Lots", new[] { "Category_CategoryId" });
            AlterColumn("dbo.Lots", "CurrentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Lots", "IsCompleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Lots", "Description", c => c.String());
            AlterColumn("dbo.Lots", "Name", c => c.String());
            DropColumn("dbo.Lots", "Category_CategoryId");
            DropColumn("dbo.Lots", "ImageMimeType");
            DropColumn("dbo.Lots", "ImageData");
            DropTable("dbo.Categories");
        }
    }
}

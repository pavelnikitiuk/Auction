namespace Auction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lots", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Images", "Lot_LotID", "dbo.Lots");
            DropIndex("dbo.Lots", new[] { "Category_CategoryId" });
            DropIndex("dbo.Images", new[] { "Lot_LotID" });
            AlterColumn("dbo.Lots", "Category_CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Images", "Lot_LotID", c => c.Int(nullable: false));
            CreateIndex("dbo.Lots", "Category_CategoryId");
            CreateIndex("dbo.Images", "Lot_LotID");
            AddForeignKey("dbo.Lots", "Category_CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Images", "Lot_LotID", "dbo.Lots", "LotID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Lot_LotID", "dbo.Lots");
            DropForeignKey("dbo.Lots", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Images", new[] { "Lot_LotID" });
            DropIndex("dbo.Lots", new[] { "Category_CategoryId" });
            AlterColumn("dbo.Images", "Lot_LotID", c => c.Int());
            AlterColumn("dbo.Lots", "Category_CategoryId", c => c.Int());
            CreateIndex("dbo.Images", "Lot_LotID");
            CreateIndex("dbo.Lots", "Category_CategoryId");
            AddForeignKey("dbo.Images", "Lot_LotID", "dbo.Lots", "LotID");
            AddForeignKey("dbo.Lots", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}

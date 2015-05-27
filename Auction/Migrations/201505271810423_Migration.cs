namespace Auction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        BidID = c.Int(nullable: false, identity: true),
                        BidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DatePlaced = c.DateTime(nullable: false),
                        UserId = c.String(),
                        LotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BidID)
                .ForeignKey("dbo.Lots", t => t.LotId, cascadeDelete: true)
                .Index(t => t.LotId);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        LotID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EndTime = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.LotID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        Lot_LotID = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Lots", t => t.Lot_LotID)
                .Index(t => t.Lot_LotID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Lot_LotID", "dbo.Lots");
            DropForeignKey("dbo.Lots", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Bids", "LotId", "dbo.Lots");
            DropIndex("dbo.Images", new[] { "Lot_LotID" });
            DropIndex("dbo.Lots", new[] { "Category_CategoryId" });
            DropIndex("dbo.Bids", new[] { "LotId" });
            DropTable("dbo.Images");
            DropTable("dbo.Categories");
            DropTable("dbo.Lots");
            DropTable("dbo.Bids");
        }
    }
}

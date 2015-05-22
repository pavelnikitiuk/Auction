namespace Auction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image : DbMigration
    {
        public override void Up()
        {
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
            
            DropColumn("dbo.Lots", "ImageData");
            DropColumn("dbo.Lots", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "ImageMimeType", c => c.String());
            AddColumn("dbo.Lots", "ImageData", c => c.Binary());
            DropForeignKey("dbo.Images", "Lot_LotID", "dbo.Lots");
            DropIndex("dbo.Images", new[] { "Lot_LotID" });
            DropTable("dbo.Images");
        }
    }
}

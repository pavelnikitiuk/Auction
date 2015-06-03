namespace Auction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bids", "Lot_LotID", "dbo.Lots");
            DropIndex("dbo.Bids", new[] { "Lot_LotID" });
            AlterColumn("dbo.Bids", "Lot_LotID", c => c.Int(nullable: false));
            CreateIndex("dbo.Bids", "Lot_LotID");
            AddForeignKey("dbo.Bids", "Lot_LotID", "dbo.Lots", "LotID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "Lot_LotID", "dbo.Lots");
            DropIndex("dbo.Bids", new[] { "Lot_LotID" });
            AlterColumn("dbo.Bids", "Lot_LotID", c => c.Int());
            CreateIndex("dbo.Bids", "Lot_LotID");
            AddForeignKey("dbo.Bids", "Lot_LotID", "dbo.Lots", "LotID");
        }
    }
}

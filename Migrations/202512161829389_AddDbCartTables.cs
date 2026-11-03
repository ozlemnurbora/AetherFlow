namespace AetherFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDbCartTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbCartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DbCartId = c.Int(nullable: false),
                        DestinationId = c.Int(nullable: false),
                        TourName = c.String(nullable: false),
                        CityName = c.String(),
                        ImageUrl = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbCarts", t => t.DbCartId, cascadeDelete: true)
                .Index(t => t.DbCartId);
            
            CreateTable(
                "dbo.DbCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DbCartItems", "DbCartId", "dbo.DbCarts");
            DropIndex("dbo.DbCartItems", new[] { "DbCartId" });
            DropTable("dbo.DbCarts");
            DropTable("dbo.DbCartItems");
        }
    }
}

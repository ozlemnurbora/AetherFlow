namespace AetherFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        DestinationId = c.Int(nullable: false),
                        TourName = c.String(),
                        CityName = c.String(),
                        ImageUrl = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        People = c.Int(nullable: false),
                        LineTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingCode = c.String(),
                        UserId = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServiceFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FullName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingItems", "BookingId", "dbo.Bookings");
            DropIndex("dbo.BookingItems", new[] { "BookingId" });
            DropTable("dbo.Bookings");
            DropTable("dbo.BookingItems");
        }
    }
}

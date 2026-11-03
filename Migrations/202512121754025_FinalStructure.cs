namespace AetherFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalStructure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Destinations", "TourName", c => c.String(nullable: false));
            AddColumn("dbo.Destinations", "CityName", c => c.String(nullable: false));
            AddColumn("dbo.Destinations", "WhatsIncluded", c => c.String(nullable: false));
            AddColumn("dbo.Destinations", "MeetingPoint", c => c.String(nullable: false));
            AddColumn("dbo.Destinations", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Destinations", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Destinations", "Duration", c => c.String(nullable: false));
            AddColumn("dbo.Destinations", "Transportation", c => c.String(nullable: false));
            AddColumn("dbo.Destinations", "Capacity", c => c.Int(nullable: false));
            DropColumn("dbo.Destinations", "City");
            DropColumn("dbo.Destinations", "PersonCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Destinations", "PersonCount", c => c.Int(nullable: false));
            AddColumn("dbo.Destinations", "City", c => c.String(nullable: false));
            DropColumn("dbo.Destinations", "Capacity");
            DropColumn("dbo.Destinations", "Transportation");
            DropColumn("dbo.Destinations", "Duration");
            DropColumn("dbo.Destinations", "EndDate");
            DropColumn("dbo.Destinations", "StartDate");
            DropColumn("dbo.Destinations", "MeetingPoint");
            DropColumn("dbo.Destinations", "WhatsIncluded");
            DropColumn("dbo.Destinations", "CityName");
            DropColumn("dbo.Destinations", "TourName");
        }
    }
}

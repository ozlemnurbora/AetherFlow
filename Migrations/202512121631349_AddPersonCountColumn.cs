namespace AetherFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonCountColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Destinations", "PersonCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Destinations", "PersonCount");
        }
    }
}

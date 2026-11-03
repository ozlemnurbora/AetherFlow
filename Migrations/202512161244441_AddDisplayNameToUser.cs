namespace AetherFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisplayNameToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DisplayName", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DisplayName");
        }
    }
}

namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visitFKNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visits", "Location_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Visits", "Location_ID");
            AddForeignKey("dbo.Visits", "Location_ID", "dbo.Locations", "Location_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visits", "Location_ID", "dbo.Locations");
            DropIndex("dbo.Visits", new[] { "Location_ID" });
            DropColumn("dbo.Visits", "Location_ID");
        }
    }
}

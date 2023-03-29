namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentFKlocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Location_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "Location_ID");
            AddForeignKey("dbo.Appointments", "Location_ID", "dbo.Locations", "Location_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Location_ID", "dbo.Locations");
            DropIndex("dbo.Appointments", new[] { "Location_ID" });
            DropColumn("dbo.Appointments", "Location_ID");
        }
    }
}

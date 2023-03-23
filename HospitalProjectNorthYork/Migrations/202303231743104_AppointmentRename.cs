namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentRename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "AppointDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "AppointDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "AppointmentDate");
        }
    }
}

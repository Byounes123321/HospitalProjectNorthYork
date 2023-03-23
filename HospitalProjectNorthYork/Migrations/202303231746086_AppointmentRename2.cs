namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentRename2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointmentDesc", c => c.String());
            DropColumn("dbo.Appointments", "AppointmentDecs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "AppointmentDecs", c => c.String());
            DropColumn("dbo.Appointments", "AppointmentDesc");
        }
    }
}

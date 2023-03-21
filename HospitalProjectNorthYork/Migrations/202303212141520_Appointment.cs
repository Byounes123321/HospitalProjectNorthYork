namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Appointment_ID = c.Int(nullable: false, identity: true),
                        AppointmentDecs = c.String(),
                        AppointDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Appointment_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appointments");
        }
    }
}

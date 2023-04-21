namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentFKpatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Patient_ID", c => c.Int());
            CreateIndex("dbo.Appointments", "Patient_ID");
            AddForeignKey("dbo.Appointments", "Patient_ID", "dbo.Patients", "Patient_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Patient_ID", "dbo.Patients");
            DropIndex("dbo.Appointments", new[] { "Patient_ID" });
            DropColumn("dbo.Appointments", "Patient_ID");
        }
    }
}

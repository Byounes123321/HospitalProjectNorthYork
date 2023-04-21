namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "Patient_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "Appointment_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Feedbacks", "Patient_ID");
            CreateIndex("dbo.Feedbacks", "Appointment_ID");
            AddForeignKey("dbo.Feedbacks", "Appointment_ID", "dbo.Appointments", "Appointment_ID", cascadeDelete: true);
            AddForeignKey("dbo.Feedbacks", "Patient_ID", "dbo.Patients", "Patient_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.Feedbacks", "Appointment_ID", "dbo.Appointments");
            DropIndex("dbo.Feedbacks", new[] { "Appointment_ID" });
            DropIndex("dbo.Feedbacks", new[] { "Patient_ID" });
            DropColumn("dbo.Feedbacks", "Appointment_ID");
            DropColumn("dbo.Feedbacks", "Patient_ID");
        }
    }
}

namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentFKdoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Doctor_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "Doctor_ID");
            AddForeignKey("dbo.Appointments", "Doctor_ID", "dbo.Doctors", "Doctor_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Doctor_ID", "dbo.Doctors");
            DropIndex("dbo.Appointments", new[] { "Doctor_ID" });
            DropColumn("dbo.Appointments", "Doctor_ID");
        }
    }
}

namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visitFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visits", "Patient_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Visits", "Patient_ID");
            AddForeignKey("dbo.Visits", "Patient_ID", "dbo.Patients", "Patient_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visits", "Patient_ID", "dbo.Patients");
            DropIndex("dbo.Visits", new[] { "Patient_ID" });
            DropColumn("dbo.Visits", "Patient_ID");
        }
    }
}

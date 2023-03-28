namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patient_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Patient_ID = c.Int(nullable: false, identity: true),
                        PatientName = c.String(),
                        PatientAdmittanceDate = c.DateTime(nullable: false),
                        PatientDateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Patient_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Patients");
        }
    }
}

namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Doctors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Doctor_ID = c.Int(nullable: false, identity: true),
                        DoctorName = c.String(),
                        DoctorBio = c.String(),
                    })
                .PrimaryKey(t => t.Doctor_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Doctors");
        }
    }
}

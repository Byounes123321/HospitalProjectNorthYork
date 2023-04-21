namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Department_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Doctors", "Department_ID");
            AddForeignKey("dbo.Doctors", "Department_ID", "dbo.Departments", "Department_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "Department_ID", "dbo.Departments");
            DropIndex("dbo.Doctors", new[] { "Department_ID" });
            DropColumn("dbo.Doctors", "Department_ID");
        }
    }
}

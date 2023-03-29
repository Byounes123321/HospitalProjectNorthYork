namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentsLocations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocationDepartments",
                c => new
                    {
                        Location_Location_ID = c.Int(nullable: false),
                        Department_Department_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Location_Location_ID, t.Department_Department_ID })
                .ForeignKey("dbo.Locations", t => t.Location_Location_ID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Department_Department_ID, cascadeDelete: true)
                .Index(t => t.Location_Location_ID)
                .Index(t => t.Department_Department_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocationDepartments", "Department_Department_ID", "dbo.Departments");
            DropForeignKey("dbo.LocationDepartments", "Location_Location_ID", "dbo.Locations");
            DropIndex("dbo.LocationDepartments", new[] { "Department_Department_ID" });
            DropIndex("dbo.LocationDepartments", new[] { "Location_Location_ID" });
            DropTable("dbo.LocationDepartments");
        }
    }
}

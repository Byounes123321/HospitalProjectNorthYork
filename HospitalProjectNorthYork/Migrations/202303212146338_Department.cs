namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Department : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Department_ID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        DepartmentDesc = c.String(),
                    })
                .PrimaryKey(t => t.Department_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Departments");
        }
    }
}

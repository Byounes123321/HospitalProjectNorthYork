namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentsFAQs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FAQDepartments",
                c => new
                    {
                        FAQ_Faq_ID = c.Int(nullable: false),
                        Department_Department_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FAQ_Faq_ID, t.Department_Department_ID })
                .ForeignKey("dbo.FAQs", t => t.FAQ_Faq_ID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Department_Department_ID, cascadeDelete: true)
                .Index(t => t.FAQ_Faq_ID)
                .Index(t => t.Department_Department_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FAQDepartments", "Department_Department_ID", "dbo.Departments");
            DropForeignKey("dbo.FAQDepartments", "FAQ_Faq_ID", "dbo.FAQs");
            DropIndex("dbo.FAQDepartments", new[] { "Department_Department_ID" });
            DropIndex("dbo.FAQDepartments", new[] { "FAQ_Faq_ID" });
            DropTable("dbo.FAQDepartments");
        }
    }
}

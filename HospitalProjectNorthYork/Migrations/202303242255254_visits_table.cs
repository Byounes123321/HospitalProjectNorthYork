namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visits_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Visit_ID = c.Int(nullable: false, identity: true),
                        VisitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Visit_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Visits");
        }
    }
}

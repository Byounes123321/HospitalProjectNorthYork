namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Location_ID = c.Int(nullable: false, identity: true),
                        LocaitonName = c.String(),
                        LocationDesc = c.String(),
                    })
                .PrimaryKey(t => t.Location_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Locations");
        }
    }
}

namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FAQ_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FAQs",
                c => new
                    {
                        Faq_ID = c.Int(nullable: false, identity: true),
                        Ques = c.String(),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Faq_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FAQs");
        }
    }
}

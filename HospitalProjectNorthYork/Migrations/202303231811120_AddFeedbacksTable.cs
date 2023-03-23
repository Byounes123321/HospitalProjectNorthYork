namespace HospitalProjectNorthYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeedbacksTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Feedback_ID = c.Int(nullable: false, identity: true),
                        FeedbackDesc = c.String(),
                    })
                .PrimaryKey(t => t.Feedback_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Feedbacks");
        }
    }
}

namespace ElectionDatAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVotes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CandidateIndex = c.Int(),
                        PeselHashCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.PeselHashCode);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Votes", new[] { "PeselHashCode" });
            DropTable("dbo.Votes");
        }
    }
}

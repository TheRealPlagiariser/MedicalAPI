namespace Medical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        batch_id = c.Int(nullable: false, identity: true),
                        batch_date_from = c.DateTime(),
                        batch_date_to = c.DateTime(),
                    })
                .PrimaryKey(t => t.batch_id);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        claim_id = c.Int(nullable: false, identity: true),
                        claim_date = c.DateTime(nullable: false),
                        emp_id = c.Int(nullable: false),
                        first_name = c.String(),
                        last_name = c.String(),
                        batch_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.claim_id)
                .ForeignKey("dbo.Batches", t => t.batch_id, cascadeDelete: true)
                .Index(t => t.batch_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Claims", "batch_id", "dbo.Batches");
            DropIndex("dbo.Claims", new[] { "batch_id" });
            DropTable("dbo.Claims");
            DropTable("dbo.Batches");
        }
    }
}

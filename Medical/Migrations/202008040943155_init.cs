namespace Medical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        emp_id = c.Int(nullable: false),
                        first_name = c.String(),
                        last_name = c.String(),
                        number_of_claims = c.Int(nullable: false),
                        claim_date = c.DateTime(nullable: false),
                        batch_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.claim_id)
                .ForeignKey("dbo.Batches", t => t.batch_id, cascadeDelete: true)
                .Index(t => t.batch_id);
            
            CreateTable(
                "dbo.hrs",
                c => new
                    {
                        emp_id = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.emp_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Claims", "batch_id", "dbo.Batches");
            DropIndex("dbo.Claims", new[] { "batch_id" });
            DropTable("dbo.hrs");
            DropTable("dbo.Claims");
            DropTable("dbo.Batches");
        }
    }
}

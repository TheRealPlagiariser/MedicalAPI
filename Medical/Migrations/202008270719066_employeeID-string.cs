namespace Medical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeeIDstring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Claims", "emp_id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Claims", "emp_id", c => c.Int(nullable: false));
        }
    }
}

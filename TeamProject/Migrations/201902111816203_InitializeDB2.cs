namespace TeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDB2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branch", "Longtitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Branch", "Latitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Branch", "Latitude", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AlterColumn("dbo.Branch", "Longtitude", c => c.Decimal(nullable: false, precision: 6, scale: 3));
        }
    }
}

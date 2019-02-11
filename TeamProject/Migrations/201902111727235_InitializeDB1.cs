namespace TeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Court", "ImageCourt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Court", "ImageCourt");
        }
    }
}

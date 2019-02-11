namespace TeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeSystemDiagram : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.sysdiagrams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
    }
}

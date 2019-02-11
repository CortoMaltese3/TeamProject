namespace TeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourtID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        BookedAt = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Court", t => t.CourtID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.CourtID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Court",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BranchID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        MaxPlayers = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branch", t => t.BranchID, cascadeDelete: true)
                .Index(t => t.BranchID);
            
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Longtitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Point = c.Decimal(nullable: false, precision: 18, scale: 2),
                        City = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 200),
                        ZipCode = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: false)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 20),
                        Lastname = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BranchID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(maxLength: 250),
                        CommentAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branch", t => t.BranchID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.BranchID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Role = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Facility",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TimeSlot",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourtID = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Hour = c.Time(nullable: false, precision: 7),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Court", t => t.CourtID, cascadeDelete: true)
                .Index(t => t.CourtID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FacilityBranches",
                c => new
                    {
                        Facility_ID = c.Int(nullable: false),
                        Branch_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Facility_ID, t.Branch_ID })
                .ForeignKey("dbo.Facility", t => t.Facility_ID, cascadeDelete: true)
                .ForeignKey("dbo.Branch", t => t.Branch_ID, cascadeDelete: true)
                .Index(t => t.Facility_ID)
                .Index(t => t.Branch_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TimeSlot", "CourtID", "dbo.Court");
            DropForeignKey("dbo.FacilityBranches", "Branch_ID", "dbo.Branch");
            DropForeignKey("dbo.FacilityBranches", "Facility_ID", "dbo.Facility");
            DropForeignKey("dbo.Court", "BranchID", "dbo.Branch");
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.User");
            DropForeignKey("dbo.Review", "UserID", "dbo.User");
            DropForeignKey("dbo.Review", "BranchID", "dbo.Branch");
            DropForeignKey("dbo.Company", "UserID", "dbo.User");
            DropForeignKey("dbo.Booking", "UserID", "dbo.User");
            DropForeignKey("dbo.Branch", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Booking", "CourtID", "dbo.Court");
            DropIndex("dbo.FacilityBranches", new[] { "Branch_ID" });
            DropIndex("dbo.FacilityBranches", new[] { "Facility_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TimeSlot", new[] { "CourtID" });
            DropIndex("dbo.UserRoles", new[] { "UserID" });
            DropIndex("dbo.Review", new[] { "UserID" });
            DropIndex("dbo.Review", new[] { "BranchID" });
            DropIndex("dbo.Company", new[] { "UserID" });
            DropIndex("dbo.Branch", new[] { "CompanyID" });
            DropIndex("dbo.Court", new[] { "BranchID" });
            DropIndex("dbo.Booking", new[] { "UserID" });
            DropIndex("dbo.Booking", new[] { "CourtID" });
            DropTable("dbo.FacilityBranches");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TimeSlot");
            DropTable("dbo.Facility");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Review");
            DropTable("dbo.User");
            DropTable("dbo.Company");
            DropTable("dbo.Branch");
            DropTable("dbo.Court");
            DropTable("dbo.Booking");
        }
    }
}

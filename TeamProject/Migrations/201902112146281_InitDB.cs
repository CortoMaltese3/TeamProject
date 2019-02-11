namespace TeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
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
                .ForeignKey("dbo.Court", t => t.CourtID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.CourtID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Court",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BranchID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ImageCourt = c.String(),
                        MaxPlayers = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 5, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branch", t => t.BranchID)
                .Index(t => t.BranchID);
            
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Longtitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        City = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 200),
                        ZipCode = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: false)
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
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.Branch", t => t.BranchID)
                .Index(t => t.BranchID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 20, unicode: false),
                        Lastname = c.String(nullable: false, maxLength: 20, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Role = c.String(nullable: false, maxLength: 5, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID);
            
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
                .ForeignKey("dbo.Court", t => t.CourtID)
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
                "dbo.BranchFacilities",
                c => new
                    {
                        BrancID = c.Int(nullable: false),
                        FacilityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BrancID, t.FacilityID })
                .ForeignKey("dbo.Branch", t => t.BrancID, cascadeDelete: true)
                .ForeignKey("dbo.Facility", t => t.FacilityID, cascadeDelete: true)
                .Index(t => t.BrancID)
                .Index(t => t.FacilityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TimeSlot", "CourtID", "dbo.Court");
            DropForeignKey("dbo.Branch", "UserID", "dbo.User");
            DropForeignKey("dbo.Review", "BranchID", "dbo.Branch");
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.User");
            DropForeignKey("dbo.Review", "UserID", "dbo.User");
            DropForeignKey("dbo.Booking", "UserID", "dbo.User");
            DropForeignKey("dbo.BranchFacilities", "FacilityID", "dbo.Facility");
            DropForeignKey("dbo.BranchFacilities", "BrancID", "dbo.Branch");
            DropForeignKey("dbo.Court", "BranchID", "dbo.Branch");
            DropForeignKey("dbo.Booking", "CourtID", "dbo.Court");
            DropIndex("dbo.BranchFacilities", new[] { "FacilityID" });
            DropIndex("dbo.BranchFacilities", new[] { "BrancID" });
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
            DropIndex("dbo.Branch", new[] { "UserID" });
            DropIndex("dbo.Court", new[] { "BranchID" });
            DropIndex("dbo.Booking", new[] { "UserID" });
            DropIndex("dbo.Booking", new[] { "CourtID" });
            DropTable("dbo.BranchFacilities");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TimeSlot");
            DropTable("dbo.UserRoles");
            DropTable("dbo.User");
            DropTable("dbo.Review");
            DropTable("dbo.Facility");
            DropTable("dbo.Branch");
            DropTable("dbo.Court");
            DropTable("dbo.Booking");
        }
    }
}

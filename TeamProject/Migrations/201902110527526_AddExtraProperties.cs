namespace TeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExtraProperties : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FacilityBranches", newName: "BranchFacilities");
            DropForeignKey("dbo.Booking", "CourtID", "dbo.Court");
            DropForeignKey("dbo.Booking", "UserID", "dbo.User");
            DropForeignKey("dbo.Court", "BranchID", "dbo.Branch");
            DropForeignKey("dbo.TimeSlot", "CourtID", "dbo.Court");
            DropForeignKey("dbo.Branch", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Review", "BranchID", "dbo.Branch");
            DropForeignKey("dbo.Company", "UserID", "dbo.User");
            DropForeignKey("dbo.Review", "UserID", "dbo.User");
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.User");
            RenameColumn(table: "dbo.BranchFacilities", name: "Facility_ID", newName: "FacilityID");
            RenameColumn(table: "dbo.BranchFacilities", name: "Branch_ID", newName: "BrancID");
            RenameIndex(table: "dbo.BranchFacilities", name: "IX_Branch_ID", newName: "IX_BrancID");
            RenameIndex(table: "dbo.BranchFacilities", name: "IX_Facility_ID", newName: "IX_FacilityID");
            DropPrimaryKey("dbo.BranchFacilities");
            AlterColumn("dbo.Court", "Price", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Branch", "Longtitude", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AlterColumn("dbo.Branch", "Latitude", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AlterColumn("dbo.Branch", "Point", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AlterColumn("dbo.User", "Firstname", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.User", "Lastname", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.UserRoles", "Role", c => c.String(nullable: false, maxLength: 5, unicode: false));
            AddPrimaryKey("dbo.BranchFacilities", new[] { "BrancID", "FacilityID" });
            AddForeignKey("dbo.Booking", "CourtID", "dbo.Court", "ID");
            AddForeignKey("dbo.Booking", "UserID", "dbo.User", "ID");
            AddForeignKey("dbo.Court", "BranchID", "dbo.Branch", "ID");
            AddForeignKey("dbo.TimeSlot", "CourtID", "dbo.Court", "ID");
            AddForeignKey("dbo.Branch", "CompanyID", "dbo.Company", "ID");
            AddForeignKey("dbo.Review", "BranchID", "dbo.Branch", "ID");
            AddForeignKey("dbo.Company", "UserID", "dbo.User", "ID");
            AddForeignKey("dbo.Review", "UserID", "dbo.User", "ID");
            AddForeignKey("dbo.UserRoles", "UserID", "dbo.User", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.User");
            DropForeignKey("dbo.Review", "UserID", "dbo.User");
            DropForeignKey("dbo.Company", "UserID", "dbo.User");
            DropForeignKey("dbo.Review", "BranchID", "dbo.Branch");
            DropForeignKey("dbo.Branch", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.TimeSlot", "CourtID", "dbo.Court");
            DropForeignKey("dbo.Court", "BranchID", "dbo.Branch");
            DropForeignKey("dbo.Booking", "UserID", "dbo.User");
            DropForeignKey("dbo.Booking", "CourtID", "dbo.Court");
            DropPrimaryKey("dbo.BranchFacilities");
            AlterColumn("dbo.UserRoles", "Role", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.User", "Lastname", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.User", "Firstname", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Branch", "Point", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Branch", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Branch", "Longtitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Court", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddPrimaryKey("dbo.BranchFacilities", new[] { "Facility_ID", "Branch_ID" });
            RenameIndex(table: "dbo.BranchFacilities", name: "IX_FacilityID", newName: "IX_Facility_ID");
            RenameIndex(table: "dbo.BranchFacilities", name: "IX_BrancID", newName: "IX_Branch_ID");
            RenameColumn(table: "dbo.BranchFacilities", name: "BrancID", newName: "Branch_ID");
            RenameColumn(table: "dbo.BranchFacilities", name: "FacilityID", newName: "Facility_ID");
            AddForeignKey("dbo.UserRoles", "UserID", "dbo.User", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Review", "UserID", "dbo.User", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Company", "UserID", "dbo.User", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Review", "BranchID", "dbo.Branch", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Branch", "CompanyID", "dbo.Company", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TimeSlot", "CourtID", "dbo.Court", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Court", "BranchID", "dbo.Branch", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Booking", "UserID", "dbo.User", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Booking", "CourtID", "dbo.Court", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.BranchFacilities", newName: "FacilityBranches");
        }
    }
}

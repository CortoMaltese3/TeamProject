using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class ProjectDbContext//:DbContext
    {

        // public ProjectDbContext()
        //: base("DefaultConnection")
        // {
        // }

        // public virtual DbSet<Booking> Booking { get; set; }
        // public virtual DbSet<Branch> Branch { get; set; }
        // public virtual DbSet<Company> Company { get; set; }
        // public virtual DbSet<Court> Court { get; set; }
        // public virtual DbSet<Facility> Facility { get; set; }
        // public virtual DbSet<Review> Review { get; set; }
        // public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        // public virtual DbSet<TimeSlot> TimeSlot { get; set; }
        // public virtual DbSet<User> User { get; set; }
        // public virtual DbSet<UserRoles> UserRoles { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Branch>()
        //        .Property(e => e.Longtitude)
        //        .HasPrecision(6, 3);

        //    modelBuilder.Entity<Branch>()
        //        .Property(e => e.Latitude)
        //        .HasPrecision(6, 3);

        //    modelBuilder.Entity<Branch>()
        //        .Property(e => e.Point)
        //        .HasPrecision(6, 3);

        //    modelBuilder.Entity<Branch>()
        //        .HasMany(e => e.Court)
        //        .WithRequired(e => e.Branch)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Branch>()
        //        .HasMany(e => e.Review)
        //        .WithRequired(e => e.Branch)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Branch>()
        //        .HasMany(e => e.Facility)
        //        .WithMany(e => e.Branch)
        //        .Map(m => m.ToTable("BranchFacilities").MapLeftKey("BrancID").MapRightKey("FacilityID"));

        //    modelBuilder.Entity<Company>()
        //        .HasMany(e => e.Branch)
        //        .WithRequired(e => e.Company)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Court>()
        //        .Property(e => e.Price)
        //        .HasPrecision(5, 2);

        //    modelBuilder.Entity<Court>()
        //        .HasMany(e => e.Booking)
        //        .WithRequired(e => e.Court)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Court>()
        //        .HasMany(e => e.TimeSlot)
        //        .WithRequired(e => e.Court)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<User>()
        //        .Property(e => e.Firstname)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<User>()
        //        .Property(e => e.Lastname)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<User>()
        //        .Property(e => e.Email)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<User>()
        //        .Property(e => e.Password)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<User>()
        //        .HasMany(e => e.Booking)
        //        .WithRequired(e => e.User)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<User>()
        //        .HasMany(e => e.Company)
        //        .WithRequired(e => e.User)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<User>()
        //        .HasMany(e => e.Review)
        //        .WithRequired(e => e.User)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<User>()
        //        .HasMany(e => e.UserRoles)
        //        .WithRequired(e => e.User)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<UserRoles>()
        //        .Property(e => e.Role)
        //        .IsUnicode(false);
        //}

    }
}
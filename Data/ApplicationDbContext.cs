using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GroupCCP.Models;
using Microsoft.AspNetCore.Identity;

namespace GroupCCP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GroupCCP.Models.Group> Group { get; set; }
        public DbSet<GroupCCP.Models.Company> Company { get; set; }
        public DbSet<GroupCCP.Models.Level> Level { get; set; }
        public DbSet<GroupCCP.Models.LevelCategory> LevelCategory { get; set; }
        public DbSet<GroupCCP.Models.ComplaintReceiveMeans> ComplaintReceiveMeans { get; set; }
        public DbSet<GroupCCP.Models.ComplaintLogStatus> ComplaintLogStatus { get; set; }
        public DbSet<GroupCCP.Models.ComplaintLogDetail> ComplaintLogDetail { get; set; }
        public DbSet<GroupCCP.Models.ComplaintCustomerInfo> ComplaintCustomerInfo { get; set; }
        public DbSet<GroupCCP.Models.StaffAccount> StaffAccount { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brands>(entity =>
            {
                entity.HasKey(x => x.BrandId);
                entity.Property(x => x.Brand);
                entity.HasOne(x => x.Company)
                    .WithMany(x => x.Brands)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<ComplaintVehicleInfo>(entity =>
            {
                entity.HasKey(x => x.VehicleId);
                entity.Property(x => x.RegistrationNumber);
                entity.HasOne(x => x.Brands)
                    .WithMany(x => x.VehicleInfos)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<ComplaintCorrectiveInfo>(entity =>
            {
                entity.HasKey(x => x.CorrectiveId);
                entity.HasOne(x => x.Log)
                    .WithMany(x => x.Correctives)
                    .HasForeignKey(x => x.LogId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<FollowUpCalls>(entity =>
            {
                entity.HasKey(x => x.FollowUpId);
                entity.HasOne(x => x.Company)
                    .WithMany(x => x.FollowUps)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<ComplaintFollowUp>(entity =>
            {
                entity.HasKey(x => x.FollowUpId);
                entity.HasOne(x => x.FollowUps)
                    .WithMany(x => x.ComplaintFollowUps)
                    .HasForeignKey(x => x.FollowUpId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.Logs)
                    .WithMany(x => x.ComplaintFollowUps)
                    .HasForeignKey(x => x.LogId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(x => x.Email);
                entity.HasMany(x => x.StaffAccounts)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired(true);
            });
            modelBuilder.Entity<StaffAccount>(entity =>
            {
                entity.HasKey(x => x.AccountId);
            });

                modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(x => x.RoleId);
                entity.HasOne(x => x.Company)
                    .WithMany(x => x.Roles)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.HasKey(x => x.PermissionId);
            });

            modelBuilder.Entity<PermissionAssignment>(entity =>
            {
                entity.HasKey(x => x.AssignmentId);
                entity.HasOne(x => x.Permissions)
                    .WithMany(x => x.Assignments)
                    .HasForeignKey(x => x.PermissionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.Roles)
                    .WithMany(x => x.Assignments)
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<RoleAssignment>(entity =>
            {
                entity.HasKey(x => x.RoleAssignmentId);
                entity.HasOne(x => x.Staff)
                    .WithMany(x => x.RolesAssignments)
                    .HasForeignKey(x => x.StaffId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.Roles)
                    .WithMany(x => x.RoleAssignments)
                    .HasForeignKey(x => x.RoleID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<ComplaintAssignment>(entity =>
            {
                entity.HasKey(x => x.AssignmentId);
                entity.HasOne(x => x.Staff)
                    .WithMany(x => x.Assignments)
                    .HasForeignKey(x => x.StaffAssigned)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.Log)
                    .WithMany(x => x.Assignments)
                    .HasForeignKey(x => x.LogId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<LevelCategory>(entity =>
            {
                entity.HasKey(x => x.LevelCategoryId);
                entity.Property(x => x.CategorName);
                entity.HasOne(x => x.ParentCategory)
                    .WithMany(x => x.ChildCategories)
                    .HasForeignKey(x => x.ParentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.HasKey(x => x.LevelId);
                entity.Property(x => x.LevelName);
                entity.HasOne(x => x.ParentLevel)
                    .WithMany(x => x.ChildLevels)
                    .HasForeignKey(x => x.ParentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });

            modelBuilder.Entity<ComplaintLogDetail>(entity =>
            {
                entity.HasKey(x => x.LogId);

                entity.HasOne(x => x.Customers)
                    .WithMany(x => x.Logs)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(x => x.LogCustomerId);

                entity.HasOne(x => x.Means )
                    .WithMany(x => x.Logs)
                    .HasForeignKey(x => x.LogMeansId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.Level)
                    .WithMany(x => x.Logs)
                    .HasForeignKey(x => x.LogLevelId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.Status)
                    .WithMany(x => x.Logs)
                    .HasForeignKey(x => x.LogStatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<ComplaintLogStatus>(entity =>
            {
                entity.HasKey(x => x.StatusId);
                entity.HasData(
                    new {StatusId = 1, Status = "Open"},
                    new { StatusId = 2, Status = "Assigned" },
                    new { StatusId = 3, Status = "WIP" },
                    new { StatusId = 4, Status = "Resolved" },
                    new { StatusId = 5, Status = "Closed" },
                    new { StatusId = 6, Status = "Hold" },
                    new { StatusId = 7, Status = "Rejected" }
                    );
            });

            modelBuilder.Entity<ComplaintReceiveMeans>(entity =>
            {
                entity.HasKey(x => x.MeansId);
            });

            modelBuilder.Entity<ComplaintCustomerInfo>(entity =>
            {
                entity.HasKey(x => x.CustomerId);
            });
        }
        
    }
}


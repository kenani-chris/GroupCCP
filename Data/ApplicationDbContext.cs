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
        public DbSet<GroupCCP.Models.Brands> Brands { get; set; }
        public DbSet<GroupCCP.Models.ComplaintLogStatus> ComplaintLogStatus { get; set; }
        public DbSet<GroupCCP.Models.ComplaintAssignment> ComplaintAssignment { get; set; }
        public DbSet<GroupCCP.Models.ComplaintLogDetail> ComplaintLogDetail { get; set; }
        public DbSet<GroupCCP.Models.ComplaintCustomerInfo> ComplaintCustomerInfo { get; set; }
        public DbSet<GroupCCP.Models.StaffAccount> StaffAccount { get; set; }
        public DbSet<GroupCCP.Models.LevelMembership> LevelMemberships{ get; set; }
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


            modelBuilder.Entity<ComplaintCorrectiveInfo>(entity =>
            {
                entity.HasKey(x => x.CorrectiveId);
                entity.HasOne(x => x.Log)
                    .WithMany(x => x.Correctives)
                    .HasForeignKey(x => x.LogId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.StaffAccount)
                    .WithMany(x => x.ComplaintCorrectiveInfos)
                    .HasForeignKey(x => x.StaffId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.ComplaintProductComponent)
                    .WithMany(x => x.ComplaintCorrectiveInfos)
                    .HasForeignKey(x => x.CorrectiveComponentId)
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

                entity.HasOne(x => x.FollowUpCalls)
                    .WithMany(x => x.ComplaintFollowUps)
                    .HasForeignKey(x => x.FollowUpTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.Staff)
                    .WithMany(x => x.FollowUps)
                    .HasForeignKey(x => x.StaffId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.Log)
                    .WithMany(x => x.FollowUps)
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

            modelBuilder.Entity<ComplaintProductComponent>(entity =>
            {
                entity.HasKey(x => x.ProductID);
                entity.HasData(
                    new { ProductID = 1, ProductComponent = "Unknown" },
                    new { ProductID = 2, ProductComponent = "Engine" },
                    new { ProductID = 3, ProductComponent = "Clutch and Transmission" },
                    new { ProductID = 4, ProductComponent = "Chasis" },
                    new { ProductID = 5, ProductComponent = "Steering and Tyre" },
                    new { ProductID = 6, ProductComponent = "Brake" },
                    new { ProductID = 7, ProductComponent = "Body" },
                    new { ProductID = 8, ProductComponent = "Body Interior" },
                    new { ProductID = 9, ProductComponent = "Door/Window/Sunroof" },
                    new { ProductID = 10, ProductComponent = "Electrical" },
                    new { ProductID = 11, ProductComponent = "Hybrid ElectVehicle" },
                    new { ProductID = 12, ProductComponent = "Special Vehicle" },
                    new { ProductID = 13, ProductComponent = "Accessories" },
                    new { ProductID = 14, ProductComponent = "Other" }
                    );
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
                entity.HasData(
                    new { PermissionId = 1, Entity = "Complaint - Submitted Complaints", Permission = "List"},
                    new { PermissionId = 2, Entity = "Complaint - Submitted Complaints", Permission = "Close"},
                    new { PermissionId = 3, Entity = "Complaint - Submitted Complaints", Permission = "Edit"},
                    new { PermissionId = 4, Entity = "Complaint - Submitted Complaints", Permission = "Delete"},
                    new { PermissionId = 5, Entity = "Complaint - Submitted Complaints", Permission = "Read"},
                    new { PermissionId = 6, Entity = "Complaint - Submitted Complaints", Permission = "Export"},
                    
                    new { PermissionId = 63, Entity = "FollowUp - Submitted Complaints", Permission = "View"},
                    new { PermissionId = 7, Entity = "FollowUp - Submitted Complaints", Permission = "Add"},
                    new { PermissionId = 8, Entity = "FollowUp - Submitted Complaints", Permission = "Edit"},
                    new { PermissionId = 9, Entity = "FollowUp - Submitted Complaints", Permission = "Delete"},

                    new { PermissionId = 64, Entity = "Corrective - Submitted Complaints", Permission = "View"},
                    new { PermissionId = 10, Entity = "Corrective - Submitted Complaints", Permission = "Add"},
                    new { PermissionId = 11, Entity = "Corrective - Submitted Complaints", Permission = "Edit"},
                    new { PermissionId = 12, Entity = "Corrective - Submitted Complaints", Permission = "Delete"},

                    new { PermissionId = 65, Entity = "Assignment - Submitted Complaints", Permission = "View" },
                    new { PermissionId = 13, Entity = "Assignment - Submitted Complaints", Permission = "Add" },
                    new { PermissionId = 14, Entity = "Assignment - Submitted Complaints", Permission = "Edit" },
                    new { PermissionId = 15, Entity = "Assignment - Submitted Complaints", Permission = "Delete" },

                    new { PermissionId = 161, Entity = "Responsibility - Submitted Complaints", Permission = "View" },
                    new { PermissionId = 162, Entity = "Responsibility - Submitted Complaints", Permission = "Add" },
                    new { PermissionId = 163, Entity = "Responsibility - Submitted Complaints", Permission = "Edit" },
                    new { PermissionId = 164, Entity = "Responsibility - Submitted Complaints", Permission = "Delete" },


                    
                    new { PermissionId = 16, Entity = "Complaint - My Complaints", Permission = "List"},
                    new { PermissionId = 17, Entity = "Complaint - My Complaints", Permission = "Close"},
                    new { PermissionId = 18, Entity = "Complaint - My Complaints", Permission = "Edit"},
                    new { PermissionId = 19, Entity = "Complaint - My Complaints", Permission = "Delete"},
                    new { PermissionId = 20, Entity = "Complaint - My Complaints", Permission = "Read"},
                    new { PermissionId = 21, Entity = "Complaint - My Complaints", Permission = "Export"},
                    
                    new { PermissionId = 66, Entity = "FollowUp - My Complaints", Permission = "View"},
                    new { PermissionId = 22, Entity = "FollowUp - My Complaints", Permission = "Add"},
                    new { PermissionId = 23, Entity = "FollowUp - My Complaints", Permission = "Edit"},
                    new { PermissionId = 24, Entity = "FollowUp - My Complaints", Permission = "Delete"},

                    new { PermissionId = 67, Entity = "Corrective - My Complaints", Permission = "View"},
                    new { PermissionId = 25, Entity = "Corrective - My Complaints", Permission = "Add"},
                    new { PermissionId = 26, Entity = "Corrective - My Complaints", Permission = "Edit"},
                    new { PermissionId = 27, Entity = "Corrective - My Complaints", Permission = "Delete"},

                    new { PermissionId = 68, Entity = "Assignment - My Complaints", Permission = "View" },
                    new { PermissionId = 28, Entity = "Assignment - My Complaints", Permission = "Add" },
                    new { PermissionId = 29, Entity = "Assignment - My Complaints", Permission = "Edit" },
                    new { PermissionId = 30, Entity = "Assignment - My Complaints", Permission = "Delete" },


                    new { PermissionId = 165, Entity = "Responsibility - My Complaints", Permission = "View" },
                    new { PermissionId = 166, Entity = "Responsibility - My Complaints", Permission = "Add" },
                    new { PermissionId = 167, Entity = "Responsibility - My Complaints", Permission = "Edit" },
                    new { PermissionId = 168, Entity = "Responsibility - My Complaints", Permission = "Delete" },



                    new { PermissionId = 31, Entity = "Complaint - Escallated Complaints", Permission = "List"},
                    new { PermissionId = 32, Entity = "Complaint - Escallated Complaints", Permission = "Close"},
                    new { PermissionId = 33, Entity = "Complaint - Escallated Complaints", Permission = "Edit"},
                    new { PermissionId = 34, Entity = "Complaint - Escallated Complaints", Permission = "Delete"},
                    new { PermissionId = 35, Entity = "Complaint - Escallated Complaints", Permission = "Read"},
                    new { PermissionId = 36, Entity = "Complaint - Escallated Complaints", Permission = "Export"},
                    
                    new { PermissionId = 69, Entity = "FollowUp - Escallated Complaints", Permission = "View"},
                    new { PermissionId = 37, Entity = "FollowUp - Escallated Complaints", Permission = "Add"},
                    new { PermissionId = 38, Entity = "FollowUp - Escallated Complaints", Permission = "Edit"},
                    new { PermissionId = 39, Entity = "FollowUp - Escallated Complaints", Permission = "Delete"},

                    new { PermissionId = 70, Entity = "Corrective - Escallated Complaints", Permission = "View"},
                    new { PermissionId = 40, Entity = "Corrective - Escallated Complaints", Permission = "Add"},
                    new { PermissionId = 41, Entity = "Corrective - Escallated Complaints", Permission = "Edit"},
                    new { PermissionId = 42, Entity = "Corrective - Escallated Complaints", Permission = "Delete"},

                    new { PermissionId = 71, Entity = "Assignment - Escallated Complaints", Permission = "View" },
                    new { PermissionId = 43, Entity = "Assignment - Escallated Complaints", Permission = "Add" },
                    new { PermissionId = 44, Entity = "Assignment - Escallated Complaints", Permission = "Edit" },
                    new { PermissionId = 45, Entity = "Assignment - Escallated Complaints", Permission = "Delete" },

                    new { PermissionId = 169, Entity = "Responsibility - Escallated Complaints", Permission = "View" },
                    new { PermissionId = 170, Entity = "Responsibility - Escallated Complaints", Permission = "Add" },
                    new { PermissionId = 171, Entity = "Responsibility - Escallated Complaints", Permission = "Edit" },
                    new { PermissionId = 172, Entity = "Responsibility - Escallated Complaints", Permission = "Delete" },



                    new { PermissionId = 46, Entity = "Complaint - Level Down Complaints", Permission = "List"},
                    new { PermissionId = 47, Entity = "Complaint - Level Down Complaints", Permission = "Close"},
                    new { PermissionId = 48, Entity = "Complaint - Level Down Complaints", Permission = "Edit"},
                    new { PermissionId = 49, Entity = "Complaint - Level Down Complaints", Permission = "Delete"},
                    new { PermissionId = 50, Entity = "Complaint - Level Down Complaints", Permission = "Read"},
                    new { PermissionId = 51, Entity = "Complaint - Level Down Complaints", Permission = "Export"},
                    
                    new { PermissionId = 72, Entity = "FollowUp - Level Down Complaints", Permission = "View"},
                    new { PermissionId = 52, Entity = "FollowUp - Level Down Complaints", Permission = "Add"},
                    new { PermissionId = 53, Entity = "FollowUp - Level Down Complaints", Permission = "Edit"},
                    new { PermissionId = 54, Entity = "FollowUp - Level Down Complaints", Permission = "Delete"},

                    new { PermissionId = 73, Entity = "Corrective - Level Down Complaints", Permission = "View"},
                    new { PermissionId = 55, Entity = "Corrective - Level Down Complaints", Permission = "Add"},
                    new { PermissionId = 56, Entity = "Corrective - Level Down Complaints", Permission = "Edit"},
                    new { PermissionId = 57, Entity = "Corrective - Level Down Complaints", Permission = "Delete"},

                    new { PermissionId = 76, Entity = "Assignment - Level Down Complaints", Permission = "View" },
                    new { PermissionId = 58, Entity = "Assignment - Level Down Complaints", Permission = "Add" },
                    new { PermissionId = 59, Entity = "Assignment - Level Down Complaints", Permission = "Edit" },
                    new { PermissionId = 60, Entity = "Assignment - Level Down Complaints", Permission = "Delete" },

                    new { PermissionId = 173, Entity = "Responsibility - Level Down Complaints", Permission = "View" },
                    new { PermissionId = 174, Entity = "Responsibility - Level Down Complaints", Permission = "Add" },
                    new { PermissionId = 175, Entity = "Responsibility - Level Down Complaints", Permission = "Edit" },
                    new { PermissionId = 176, Entity = "Responsibility - Level Down Complaints", Permission = "Delete" },


                    new { PermissionId = 61, Entity = "Complaint", Permission = "Add" },
                    new { PermissionId = 62, Entity = "Customer", Permission = "Add" },
                    new { PermissionId = 160, Entity = "Vehicle", Permission = "Add" },

                    // Admin Permissions
                    new { PermissionId = 77, Entity = "Admin - Home", Permission = "View" },

                    new { PermissionId = 78, Entity = "Admin - Logs", Permission = "List" },
                    new { PermissionId = 79, Entity = "Admin - Logs", Permission = "Edit" },
                    new { PermissionId = 80, Entity = "Admin - Logs", Permission = "Delete" },
                    new { PermissionId = 81, Entity = "Admin - Logs", Permission = "Create" },
                    new { PermissionId = 82, Entity = "Admin - Logs", Permission = "View" },

                    new { PermissionId = 83, Entity = "Admin - Assignment", Permission = "List" },
                    new { PermissionId = 84, Entity = "Admin - Assignment", Permission = "Edit" },
                    new { PermissionId = 85, Entity = "Admin - Assignment", Permission = "Delete" },
                    new { PermissionId = 86, Entity = "Admin - Assignment", Permission = "Create" },

                    new { PermissionId = 87, Entity = "Admin - FollowUp", Permission = "List" },
                    new { PermissionId = 88, Entity = "Admin - FollowUp", Permission = "Edit" },
                    new { PermissionId = 89, Entity = "Admin - FollowUp", Permission = "Delete" },
                    new { PermissionId = 90, Entity = "Admin - FollowUp", Permission = "Create" },

                    new { PermissionId = 91, Entity = "Admin - Corrective", Permission = "List" },
                    new { PermissionId = 92, Entity = "Admin - Corrective", Permission = "Edit" },
                    new { PermissionId = 93, Entity = "Admin - Corrective", Permission = "Delete" },
                    new { PermissionId = 94, Entity = "Admin - Corrective", Permission = "Create" },

                    new { PermissionId = 95, Entity = "Admin - Customer", Permission = "List" },
                    new { PermissionId = 96, Entity = "Admin - Customer", Permission = "Edit" },
                    new { PermissionId = 97, Entity = "Admin - Customer", Permission = "Delete" },
                    new { PermissionId = 98, Entity = "Admin - Customer", Permission = "Create" },
                    new { PermissionId = 99, Entity = "Admin - Customer", Permission = "View" },

                    new { PermissionId = 100, Entity = "Admin - Brands", Permission = "List" },
                    new { PermissionId = 101, Entity = "Admin - Brands", Permission = "Edit" },
                    new { PermissionId = 102, Entity = "Admin - Brands", Permission = "Delete" },
                    new { PermissionId = 103, Entity = "Admin - Brands", Permission = "Create" },
                    new { PermissionId = 104, Entity = "Admin - Brands", Permission = "View" },
                    

                    new { PermissionId = 105, Entity = "Admin - ReceiveMeans", Permission = "List" },
                    new { PermissionId = 106, Entity = "Admin - ReceiveMeans", Permission = "Edit" },
                    new { PermissionId = 107, Entity = "Admin - ReceiveMeans", Permission = "Delete" },
                    new { PermissionId = 108, Entity = "Admin - ReceiveMeans", Permission = "Create" },
                    new { PermissionId = 109, Entity = "Admin - ReceiveMeans", Permission = "View" },
                    

                    new { PermissionId = 110, Entity = "Admin - FollowUpTypes", Permission = "List" },
                    new { PermissionId = 111, Entity = "Admin - FollowUpTypes", Permission = "Edit" },
                    new { PermissionId = 112, Entity = "Admin - FollowUpTypes", Permission = "Delete" },
                    new { PermissionId = 113, Entity = "Admin - FollowUpTypes", Permission = "Create" },
                    new { PermissionId = 114, Entity = "Admin - FollowUpTypes", Permission = "View" },
                    

                    new { PermissionId = 115, Entity = "Admin - Levels", Permission = "List" },
                    new { PermissionId = 116, Entity = "Admin - Levels", Permission = "Edit" },
                    new { PermissionId = 117, Entity = "Admin - Levels", Permission = "Delete" },
                    new { PermissionId = 118, Entity = "Admin - Levels", Permission = "Create" },
                    new { PermissionId = 119, Entity = "Admin - Levels", Permission = "View" },
                    
                    new { PermissionId = 120, Entity = "Admin - LevelCategories", Permission = "List" },
                    new { PermissionId = 121, Entity = "Admin - LevelCategories", Permission = "Edit" },
                    new { PermissionId = 122, Entity = "Admin - LevelCategories", Permission = "Delete" },
                    new { PermissionId = 123, Entity = "Admin - LevelCategories", Permission = "Create" },
                    new { PermissionId = 124, Entity = "Admin - LevelCategories", Permission = "View" },

                    new { PermissionId = 125, Entity = "Admin - StaffAccounts", Permission = "List" },
                    new { PermissionId = 126, Entity = "Admin - StaffAccounts", Permission = "Edit" },
                    new { PermissionId = 127, Entity = "Admin - StaffAccounts", Permission = "Delete" },
                    new { PermissionId = 128, Entity = "Admin - StaffAccounts", Permission = "Create" },
                    new { PermissionId = 129, Entity = "Admin - StaffAccounts", Permission = "View" },

                    new { PermissionId = 130, Entity = "Admin - LevelMemberships", Permission = "List" },
                    new { PermissionId = 131, Entity = "Admin - LevelMemberships", Permission = "Edit" },
                    new { PermissionId = 132, Entity = "Admin - LevelMemberships", Permission = "Delete" },
                    new { PermissionId = 133, Entity = "Admin - LevelMemberships", Permission = "Create" },
                    new { PermissionId = 134, Entity = "Admin - LevelMemberships", Permission = "View" },

                    new { PermissionId = 135, Entity = "Admin - RoleAssignments", Permission = "List" },
                    new { PermissionId = 136, Entity = "Admin - RoleAssignments", Permission = "Edit" },
                    new { PermissionId = 137, Entity = "Admin - RoleAssignments", Permission = "Delete" },
                    new { PermissionId = 138, Entity = "Admin - RoleAssignments", Permission = "Create" },
                    new { PermissionId = 139, Entity = "Admin - RoleAssignments", Permission = "View" },

                    new { PermissionId = 140, Entity = "Admin - PermissionAssignments", Permission = "List" },
                    new { PermissionId = 141, Entity = "Admin - PermissionAssignments", Permission = "Edit" },
                    new { PermissionId = 142, Entity = "Admin - PermissionAssignments", Permission = "Delete" },
                    new { PermissionId = 143, Entity = "Admin - PermissionAssignments", Permission = "Create" },
                    new { PermissionId = 144, Entity = "Admin - PermissionAssignments", Permission = "View" },

                    new { PermissionId = 145, Entity = "Admin - Timelines", Permission = "List" },
                    new { PermissionId = 146, Entity = "Admin - Timelines", Permission = "Edit" },
                    new { PermissionId = 147, Entity = "Admin - Timelines", Permission = "Delete" },
                    new { PermissionId = 148, Entity = "Admin - Timelines", Permission = "Create" },
                    new { PermissionId = 149, Entity = "Admin - Timelines", Permission = "View" },

                    new { PermissionId = 150, Entity = "Admin - Companies", Permission = "List" },
                    new { PermissionId = 151, Entity = "Admin - Companies", Permission = "Edit" },
                    new { PermissionId = 152, Entity = "Admin - Companies", Permission = "Delete" },
                    new { PermissionId = 153, Entity = "Admin - Companies", Permission = "Create" },
                    new { PermissionId = 154, Entity = "Admin - Companies", Permission = "View" },

                    new { PermissionId = 155, Entity = "Admin - Roles", Permission = "List" },
                    new { PermissionId = 156, Entity = "Admin - Roles", Permission = "Edit" },
                    new { PermissionId = 157, Entity = "Admin - Roles", Permission = "Delete" },
                    new { PermissionId = 158, Entity = "Admin - Roles", Permission = "Create" },
                    new { PermissionId = 159, Entity = "Admin - Roles", Permission = "View" }

                    );
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

            modelBuilder.Entity<ComplaintResponsibility>(entity =>
            {
                entity.HasKey(x => x.ResponsibilityId);
                entity.HasOne(x => x.Log)
                    .WithMany(x => x.ComplaintResponsibilities)
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

            modelBuilder.Entity<ComplaintVehicleInfo>(entity =>
            {
                entity.HasKey(x => x.VehicleId);
                entity.HasOne(x => x.Brands)
                    .WithMany(x => x.complaintVehicleInfos)
                    .HasForeignKey(x => x.VehicleBrandId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });

            modelBuilder.Entity<LevelMembership>(entity =>
            {
                entity.HasKey(x => x.MembershipId);

                entity.HasOne(x => x.Level)
                .WithMany(x => x.LevelMemberships)
                .HasForeignKey(x => x.LevelId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

                entity.HasOne(x => x.Staff)
                .WithMany(x => x.LevelMemberships)
                .HasForeignKey(x => x.StaffId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

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

                entity.HasOne(x => x.ComplaintVehicleInfo)
                    .WithMany(x => x.Logs)
                    .HasForeignKey(x => x.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.StaffAccount)
                    .WithMany(x => x.ComplaintLogDetails)
                    .HasForeignKey(x => x.StaffId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                entity.HasOne(x => x.Priority)
                    .WithMany(x => x.ComplaintLogDetails)
                    .HasForeignKey(x => x.PriorityId)
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
                entity.HasData(
                    new { MeansId = 1, Means = "Email"},
                    new { MeansId = 2, Means = "Walk In" },
                    new { MeansId = 3, Means = "Call" }
                    );
            });

            modelBuilder.Entity<ComplaintCustomerInfo>(entity =>
            {
                entity.HasKey(x => x.CustomerId);
                entity.Property(x => x.CustomerName);
                entity.HasOne(x => x.Company)
                    .WithMany(x => x.Customers)
                    .HasForeignKey(x => x.CompanyId)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(x => x.NotificationId);
                entity.HasOne(x => x.Account)
                    .WithMany(x => x.Notifications)
                    .HasForeignKey(x => x.StaffId)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Timelines>(entity =>
            {
                entity.HasKey(x => x.TimeLineId);
                entity.HasOne(x => x.Company)
                    .WithMany(x => x.Timelines)
                    .HasForeignKey(x => x.CompanyId)
                    .IsRequired(true);
                entity.HasOne(x => x.Priority)
                    .WithMany(x => x.Timelines)
                    .HasForeignKey(x => x.PriorityId)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.HasKey(x => x.PriorityId);
                entity.HasData(
                    new {PriorityId = 1, PriorityName = "Critical", PriorityCloseDate = 4F},
                    new {PriorityId = 2, PriorityName = "High", PriorityCloseDate = 8F},
                    new {PriorityId = 3, PriorityName = "Normal", PriorityCloseDate = 24F},
                    new {PriorityId = 4, PriorityName = "Low", PriorityCloseDate = 48F},
                    new {PriorityId = 5, PriorityName = "Extremely Low", PriorityCloseDate = 0F}
                    );
            });
            
            modelBuilder.Entity<OverdueReminder>(entity =>
            {
                entity.HasKey(x => x.ReminderId);
                entity.HasOne(x => x.StaffAccount)
                    .WithMany(x => x.OverdueReminders)
                    .HasForeignKey(x => x.StaffId)
                    .IsRequired(true);
                
                entity.HasOne(x => x.ComplaintLogDetail)
                    .WithMany(x => x.OverdueReminders)
                    .HasForeignKey(x => x.LogId)
                    .IsRequired(true);
            });
        }
        public DbSet<GroupCCP.Models.ComplaintCorrectiveInfo> ComplaintCorrectiveInfo { get; set; }
        public DbSet<GroupCCP.Models.FollowUpCalls> FollowUpCalls { get; set; }
        public DbSet<GroupCCP.Models.ComplaintFollowUp> ComplaintFollowUp { get; set; }
        public DbSet<GroupCCP.Models.RoleAssignment> RoleAssignment { get; set; }
        public DbSet<GroupCCP.Models.Roles> Roles { get; set; }
        public DbSet<GroupCCP.Models.PermissionAssignment> PermissionAssignment { get; set; }
        public DbSet<GroupCCP.Models.Permissions> Permissions { get; set; }
        public DbSet<GroupCCP.Models.Notification> Notification { get; set; }
        public DbSet<GroupCCP.Models.Timelines> Timelines { get; set; }
        public DbSet<GroupCCP.Models.OverdueReminder> OverdueReminder { get; set; }
        public DbSet<GroupCCP.Models.Priority> Priority { get; set; }
        public DbSet<GroupCCP.Models.ComplaintProductComponent>  ComplaintProductComponent{ get; set; }
        public DbSet<GroupCCP.Models.ComplaintVehicleInfo> ComplaintVehicleInfo { get; set; }
        public DbSet<GroupCCP.Models.ComplaintResponsibility> ComplaintResponsibility { get; set; }
        
    }
}


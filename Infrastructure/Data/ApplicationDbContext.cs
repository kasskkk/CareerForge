using Domain.JobPost;
using Domain.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Domain.JobPost.JobPost> JobPosts { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Domain.ReportedPost.ReportedJobPost>  ReportedJobPosts { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja kaskadowego usuwania dla klucza obcego UserId w tabeli JobApplications
            modelBuilder.Entity<JobApplication>()
                .HasOne(ja => ja.User)
                .WithMany()
                .HasForeignKey(ja => ja.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Wyłącz kaskadowe usuwanie dla klucza obcego JobPostId w tabeli JobApplications
            modelBuilder.Entity<JobApplication>()
                .HasOne(ja => ja.JobPost)
                .WithMany(jp => jp.JobApplications)
                .HasForeignKey(ja => ja.JobPostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

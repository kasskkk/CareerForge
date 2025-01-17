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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}

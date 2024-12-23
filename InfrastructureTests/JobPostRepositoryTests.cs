using Domain.JobPost;
using Infrastructure.Data;
using Infrastructure.JobPost;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureTests
{
    public class JobPostRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;
        public JobPostRepositoryTests()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JobPostingDb")
                .Options;
        }
        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_contextOptions);

        [Fact]
        public async Task AddAsync_ShouldAddJobPost()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var jobPost = new JobPost()
            {
                Title = "Test",
                Company = "Test",
                Description = "Test",
                Salary = "Test",
                Location = "Test",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = "TestId"
            };

            await repository.AddAsync(jobPost);

            var result = await context.JobPosts.FindAsync(jobPost.Id);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllJobPosts()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var jobPost1 = new JobPost
            {
                Title = "Test1",
                Company = "Test1",
                Description = "Tes1t1",
                Salary = "Test1",
                Location = "Test1",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = "TestId1"
            };

            var jobPost2 = new JobPost
            {
                Title = "Test2",
                Company = "Test2",
                Description = "Test2",
                Salary = "Test2",
                Location = "Test2",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = "TestId2"
            };

            await context.JobPosts.AddRangeAsync(jobPost1, jobPost2);
            await context.SaveChangesAsync();

            var result = await repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.True(result.Count () >= 2);
            Assert.Equal("Test2", result.FirstOrDefault(j => j.Title == jobPost2.Title)?.Title);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteJobPost()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var jobPost = new JobPost
            {
                Title = "Test",
                Company = "Test",
                Description = "Tes1t",
                Salary = "Test",
                Location = "Test",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = "TestId"
            };

            await context.JobPosts.AddAsync(jobPost);
            await context.SaveChangesAsync();

            await repository.DeleteAsync(jobPost.Id);

            var result = await context.JobPosts.FindAsync(jobPost.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowKeyNotFoundException()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.DeleteAsync(999));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPost()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var jobPost = new JobPost
            {
                Title = "Test",
                Company = "Test",
                Description = "Tes1t",
                Salary = "Test",
                Location = "Test",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = "TestId"
            };

            await context.JobPosts.AddAsync(jobPost);
            await context.SaveChangesAsync();

            var result = await repository.GetByIdAsync(jobPost.Id);

            Assert.NotNull(result);
            Assert.Equal("Tes1t", result.Description);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.GetByIdAsync(999));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateJobPost()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var jobPost = new JobPost
            {
                Title = "Test",
                Company = "Test",
                Description = "Tes1t",
                Salary = "Test",
                Location = "Test",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = "TestId"
            };

            await context.JobPosts.AddAsync(jobPost);
            await context.SaveChangesAsync();

            jobPost.Description = "Updated";

            await repository.UpdateAsync(jobPost);

            var result = await context.JobPosts.FindAsync(jobPost.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated", result.Description);
        }
    }
}

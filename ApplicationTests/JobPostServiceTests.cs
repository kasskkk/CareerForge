using Application.JobPost;
using Domain.JobPost;
using Infrastructure.Data;
using Infrastructure.JobPost;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests
{
    public class JobPostServiceTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;
        public JobPostServiceTests()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JobPostingDb")
                .Options;
        }
        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext( _contextOptions );

        [Fact]
        public async Task AddAsync_ShouldAddJobPost()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository( context );

            var service = new JobPostService( repository );

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

            await service.AddAsync( jobPost );

            var result = await service.GetByIdAsync( jobPost.Id );

            Assert.NotNull( result );
            Assert.Equal( "Test", result.Title );
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteJobPost()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var service = new JobPostService(repository);

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
            await service.AddAsync(jobPost);

            await service.DeleteAsync( jobPost.Id );

            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetByIdAsync(jobPost.Id));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllJobPosts()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var service = new JobPostService(repository);

            var jobPost1 = new JobPost()
            {
                Title = "Test1",
                Company = "Test1",
                Description = "Test1",
                Salary = "Test1",
                Location = "Test1",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = "TestId1"
            };

            var jobPost2 = new JobPost()
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

            await service.AddAsync(jobPost1);
            await service.AddAsync(jobPost2);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
            Assert.Equal("Test2", result.FirstOrDefault(j =>  j.Title == jobPost2.Title)?.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPost()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var service = new JobPostService(repository);

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

            await service.AddAsync(jobPost);

            var result = await service.GetByIdAsync(jobPost.Id);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateJobPost()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var service = new JobPostService(repository);

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

            await service.AddAsync(jobPost);

            jobPost.Title = "TestUpdated";

            await service.UpdateAsync(jobPost);

            var result = await service.GetByIdAsync(jobPost.Id);

            Assert.NotNull(result);
            Assert.Equal("TestUpdated", result.Title);
        }

        [Fact]
        public async Task ApproveAsync_ShouldApproveJobPost()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var service = new JobPostService(repository);

            var jobPost = new JobPost()
            {
                Title = "Test",
                Company = "Test",
                Description = "Test",
                Salary = "Test",
                Location = "Test",
                Created = DateTime.Now,
                IsApproved = false,
                UserId = "TestId"
            };

            await service.AddAsync(jobPost);

            await service.ApproveAsync(jobPost.Id);

            var result = await service.GetByIdAsync(jobPost.Id);

            Assert.NotNull(result);
            Assert.True(result.IsApproved);
        }

        [Fact]
        public async Task GetMyPosts_ShouldReturnAllUserJobPosts()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var service = new JobPostService(repository);

            var userId = "TestIdTest";

            var jobPost1 = new JobPost()
            {
                Title = "Test1",
                Company = "Test1",
                Description = "Test1",
                Salary = "Test1",
                Location = "Test1",
                Created = DateTime.Now,
                IsApproved = false,
                UserId = userId
            };

            var jobPost2 = new JobPost()
            {
                Title = "Test2",
                Company = "Test2",
                Description = "Test2",
                Salary = "Test2",
                Location = "Test2",
                Created = DateTime.Now,
                IsApproved = false,
                UserId = userId
            };

            await service.AddAsync(jobPost1);
            await service.AddAsync(jobPost2);

            var result = await service.GetMyPostsAsync(userId);

            Assert.NotNull(result);
            Assert.True(result.Count() == 2);
            Assert.Equal("Test2", result.FirstOrDefault(j => j.Title == jobPost2.Title)?.Title);
        }

        [Fact]
        public async Task GetAllApprovedAsync_ShouldReturnAllApprovedJobPosts()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var service = new JobPostService(repository);

            var userId = "TestId";

            var jobPost1 = new JobPost()
            {
                Title = "Test1",
                Company = "Test1",
                Description = "Test1",
                Salary = "Test1",
                Location = "Test1",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = userId
            };

            var jobPost2 = new JobPost()
            {
                Title = "Test2",
                Company = "Test2",
                Description = "Test2",
                Salary = "Test2",
                Location = "Test2",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = userId
            };

            var jobPost3 = new JobPost()
            {
                Title = "Test3",
                Company = "Test3",
                Description = "Test3",
                Salary = "Test3",
                Location = "Test3",
                Created = DateTime.Now,
                IsApproved = false,
                UserId = userId
            };

            await service.AddAsync(jobPost1);
            await service.AddAsync(jobPost2);
            await service.AddAsync(jobPost3);

            var result = await service.GetAllApprovedAsync();

            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
            Assert.Equal("Test2", result.FirstOrDefault(j => j.Title == jobPost2.Title)?.Title);
        }

        [Fact]
        public async Task GetAllUnapprovedAsync_ShouldReturnAllUnapprovedJobPosts()
        {
            var context = CreateDbContext();

            var repository = new JobPostRepository(context);

            var service = new JobPostService(repository);

            var userId = "TestId";

            var jobPost1 = new JobPost()
            {
                Title = "Test1",
                Company = "Test1",
                Description = "Test1",
                Salary = "Test1",
                Location = "Test1",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = userId
            };

            var jobPost2 = new JobPost()
            {
                Title = "Test2",
                Company = "Test2",
                Description = "Test2",
                Salary = "Test2",
                Location = "Test2",
                Created = DateTime.Now,
                IsApproved = true,
                UserId = userId
            };

            var jobPost3 = new JobPost()
            {
                Title = "Test3",
                Company = "Test3",
                Description = "Test3",
                Salary = "Test3",
                Location = "Test3",
                Created = DateTime.Now,
                IsApproved = false,
                UserId = userId
            };

            await service.AddAsync(jobPost1);
            await service.AddAsync(jobPost2);
            await service.AddAsync(jobPost3);

            var result = await service.GetAllUnapprovedAsync();

            Assert.NotNull(result);
            Assert.True(result.Count() >= 1);
            Assert.Equal("Test3", result.FirstOrDefault(j => j.Title == jobPost3.Title)?.Title);
        }
    }
}

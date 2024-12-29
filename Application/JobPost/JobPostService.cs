using Domain.Constant;
using Domain.Generic;
using Domain.JobPost;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobPost
{
    public class JobPostService : IJobPostService
    {
        private readonly IJobPostRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        public JobPostService(IJobPostRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task AddAsync(Domain.JobPost.JobPost entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id, ClaimsPrincipal user)
        {
            var jobPost = await _repository.GetByIdAsync(id);

            var userId = _userManager.GetUserId(user);

            if (userId != jobPost.UserId && !user.IsInRole(Role.ADMIN))
            {
                throw new UnauthorizedAccessException();
            }

            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Domain.JobPost.JobPost>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Domain.JobPost.JobPost> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Domain.JobPost.JobPost entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task ApproveAsync(int id)
        {
            var jobPost = await _repository.GetByIdAsync(id);

            jobPost.IsApproved = true;

            await _repository.UpdateAsync(jobPost);
        }

        public async Task<IEnumerable<Domain.JobPost.JobPost>> GetQueryable(ClaimsPrincipal user)
        {
            var userId = _userManager.GetUserId(user);

            var myPosts = _repository.GetQueryable()
                .Where(m => m.UserId == userId);

            return await Task.FromResult(myPosts.ToList());
        }

    }
}

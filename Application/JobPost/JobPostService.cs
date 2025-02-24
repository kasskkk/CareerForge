﻿using Domain.Constant;
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
        public JobPostService(IJobPostRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(Domain.JobPost.JobPost entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var jobPost = await _repository.GetByIdAsync(id);

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

        public async Task<IEnumerable<Domain.JobPost.JobPost>> GetMyPostsAsync(string _userId)
        {
            var userId = _userId;
            var jobPosts = await _repository.GetAllAsync();

            if (jobPosts == null)
            {
                throw new ArgumentNullException(nameof(jobPosts));
            }

            var myPosts = jobPosts.Where(j => j.UserId == userId).ToList();

            return myPosts;
        }

        public async Task<IEnumerable<Domain.JobPost.JobPost>> GetAllApprovedAsync()
        {
            var jobPosts = await _repository.GetAllAsync();

            if (jobPosts == null)
            {
                throw new ArgumentNullException(nameof(jobPosts));
            }

            var approvedPosts = jobPosts.Where(j => j.IsApproved == true).ToList();

            return approvedPosts;
        }

        public async Task<IEnumerable<Domain.JobPost.JobPost>> GetAllUnapprovedAsync()
        {
            var jobPosts = await _repository.GetAllAsync();

            if (jobPosts == null)
            {
                throw new ArgumentNullException(nameof(jobPosts));
            }

            var unapprovedPosts = jobPosts.Where(j => j.IsApproved == false).ToList();

            return unapprovedPosts;
        }
    }
}

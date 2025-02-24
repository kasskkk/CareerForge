﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobPost
{
    public interface IJobPostService
    {
        Task<IEnumerable<Domain.JobPost.JobPost>> GetAllAsync();
        Task<Domain.JobPost.JobPost> GetByIdAsync(int id);
        Task AddAsync(Domain.JobPost.JobPost entity);
        Task UpdateAsync(Domain.JobPost.JobPost entity);
        Task DeleteAsync(int id);
        Task ApproveAsync(int id);
        Task<IEnumerable<Domain.JobPost.JobPost>> GetMyPostsAsync(string userId);
        Task<IEnumerable<Domain.JobPost.JobPost>> GetAllApprovedAsync();
        Task<IEnumerable<Domain.JobPost.JobPost>> GetAllUnapprovedAsync();
    }
}

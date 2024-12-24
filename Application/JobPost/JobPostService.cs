using Domain.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobPost
{
    public class JobPostService : IJobPostService
    {
        private readonly IRepository<Domain.JobPost.JobPost> _repository;
        public JobPostService(IRepository<Domain.JobPost.JobPost> repository) 
        { 
            _repository = repository;
        }
        public async Task AddAsync(Domain.JobPost.JobPost entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
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
    }
}

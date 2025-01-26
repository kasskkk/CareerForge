using Domain.Generic;
using Domain.JobPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobApplication
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IRepository<Domain.JobPost.JobApplication> _repository;
        public JobApplicationService(IRepository<Domain.JobPost.JobApplication> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(Domain.JobPost.JobApplication entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Domain.JobPost.JobApplication>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Domain.JobPost.JobApplication> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Domain.JobPost.JobApplication>> GetJobApplicationsByJobPostIdAsync(int id)
        {
            var jobApplications = await _repository.GetAllAsync();
            return jobApplications
                .Where(ja => ja.JobPostId == id);
        }

        public async Task UpdateAsync(Domain.JobPost.JobApplication entity)
        {
            await _repository.UpdateAsync(entity);
        }

    }
}

using Domain.Generic;
using Domain.JobPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobApplcation
{
    public class JobAppplicationService : IJobApplicationService
    {
        private readonly IRepository<JobApplication> _repository;
        public JobAppplicationService(IRepository<JobApplication> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(JobApplication entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<JobApplication>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<JobApplication> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(JobApplication entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}

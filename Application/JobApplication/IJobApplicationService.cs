using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobApplication
{
    public interface IJobApplicationService
    {
        Task<IEnumerable<Domain.JobPost.JobApplication>> GetAllAsync();
        Task<Domain.JobPost.JobApplication> GetByIdAsync(int id);
        Task AddAsync(Domain.JobPost.JobApplication entity);
        Task UpdateAsync(Domain.JobPost.JobApplication entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<Domain.JobPost.JobApplication>> GetJobApplicationsByJobPostIdAsync(int id);
    }
}

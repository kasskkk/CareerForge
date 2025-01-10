using Application.JobPost;
using Domain.Generic;
using Domain.ReportedPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReportedPostService
{
    public class ReportedJobPostService : IReportedJobPostService
    {
        private readonly IRepository<Domain.ReportedPost.ReportedJobPost> _repository;
        public ReportedJobPostService(IRepository<Domain.ReportedPost.ReportedJobPost> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(ReportedJobPost entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReportedJobPost>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<ReportedJobPost> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ReportedJobPost entity)
        {
            throw new NotImplementedException();
        }
    }
}

using Application.JobPost;
using Domain.Constant;
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

        public async Task AddAsync(ReportedJobPost entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id, ClaimsPrincipal user)
        {
            var reportJobPost = await _repository.GetByIdAsync(id);

            if (!user.IsInRole(Role.ADMIN))
            {
                throw new UnauthorizedAccessException();
            }

            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReportedJobPost>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ReportedJobPost> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(ReportedJobPost entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}

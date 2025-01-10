using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReportedPostService
{
    public interface IReportedJobPostService
    {
        Task<IEnumerable<Domain.ReportedPost.ReportedJobPost>> GetAllAsync();
        Task<Domain.ReportedPost.ReportedJobPost> GetByIdAsync(int id);
        Task AddAsync(Domain.ReportedPost.ReportedJobPost entity);
        Task UpdateAsync(Domain.ReportedPost.ReportedJobPost entity);
        Task DeleteAsync(int id, ClaimsPrincipal user);
    }
}

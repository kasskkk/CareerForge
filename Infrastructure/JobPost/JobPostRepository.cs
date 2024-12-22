using Domain.Generic;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JobPost
{
    public class JobPostRepository : IRepository<Domain.JobPost.JobPost>
    {
        private readonly ApplicationDbContext _context;
        public JobPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Domain.JobPost.JobPost entity)
        {
            await _context.JobPosts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var jobPost = await _context.JobPosts.FindAsync(id);

            if (jobPost == null)
            {
                throw new KeyNotFoundException(nameof(jobPost));
            }

            _context.JobPosts.Remove(jobPost);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Domain.JobPost.JobPost>> GetAllAsync()
        {
            return await _context.JobPosts.ToListAsync();
        }

        public async Task<Domain.JobPost.JobPost> GetByIdAsync(int id)
        {
            var jobPost = await _context.JobPosts.FindAsync(id);

            if (jobPost == null)
            {
                throw new KeyNotFoundException(nameof(jobPost));
            }

            return jobPost;
        }

        public async Task UpdateAsync(Domain.JobPost.JobPost entity)
        {
            _context.JobPosts.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

using Domain.Generic;
using Domain.JobPost;
using Infrastructure.Data;
using Infrastructure.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JobPost
{
    public class JobCateogryRepository : GenericRepository<JobCategory>
    {
        public JobCateogryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

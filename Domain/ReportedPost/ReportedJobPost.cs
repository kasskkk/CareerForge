using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReportedPost
{
    public class ReportedJobPost
    {
        public int Id { get; set; }
        [Required]
        public ReportName Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int JobPostId { get; set; }
        [ForeignKey(nameof(JobPostId))]
        public Domain.JobPost.JobPost JobPost { get; set; }
    }
}

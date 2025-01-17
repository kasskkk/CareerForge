using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.JobPost
{
    public class JobApplication
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
        [Required]
        public int JobPostId { get; set; }
        [ForeignKey(nameof(JobPostId))]
        public JobPost JobPost { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}

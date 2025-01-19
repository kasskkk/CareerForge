using Domain.User;
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
        public int UserInfoId { get; set; }
        [ForeignKey(nameof(UserInfoId))]
        public UserInfo UserInfo { get; set; }
        [Required]
        public int JobPostId { get; set; }
        [ForeignKey(nameof(JobPostId))]
        public JobPost JobPost { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}

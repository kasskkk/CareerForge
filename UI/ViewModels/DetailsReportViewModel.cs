using Domain.Enum;
using Domain.JobPost;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class DetailsReportViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Salary { get; set; }
        public string Location { get; set; }
        public int JobCategoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
        public DateTime Created { get; set; }
        public JobCategory Category { get; set; }
        [Required]
        public ReportName ReportName { get; set; }
        [Required]
        public string ReportDescription { get; set; }
    }
}

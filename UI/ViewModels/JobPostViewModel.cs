using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class JobPostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Salary { get; set; }
        [Required]
        public string Location { get; set; }
    }
}

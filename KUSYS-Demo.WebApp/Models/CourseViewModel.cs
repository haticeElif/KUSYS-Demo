using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.WebApp.Models
{
    public class CourseViewModel
    {
        [Column(TypeName = "int")]
        [Required(ErrorMessage = "This field is required.")]
        public string CourseId { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("CourseName")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(12, ErrorMessage = "Maximum 150 characters only.")]
        public string CourseName { get; set; }
        public string ControlId { get; set; }
    }
}

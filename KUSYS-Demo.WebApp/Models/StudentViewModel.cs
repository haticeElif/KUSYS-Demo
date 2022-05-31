using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.WebApp.Models
{
    public class StudentViewModel
    {
        [Column(TypeName = "int")]
        [Required(ErrorMessage = "This field is required.")]
        public int StudentId { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("FirstName")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(12, ErrorMessage = "Maximum 150 characters only.")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("LastName")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(12, ErrorMessage = "Maximum 150 characters only.")]
        public string LastName { get; set; }

        [Column(TypeName = "datetime")]
        [DisplayName("Birth Date")]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? BirthDate { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public List<Itemlist> StudentList { get; set; }
        public List<Itemlist> CourseList { get; set; }
        public string Role { get; set; }
    }

    public class Itemlist
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}

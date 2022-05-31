﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Data.Entities
{
    public partial class Course
    {
        public Course()
        {
            Student = new HashSet<Student>();
        }

        [Key]
        [StringLength(50)]
        public string CourseId { get; set; }
        [Required]
        [StringLength(150)]
        public string CourseName { get; set; }

        [InverseProperty("Course")]
        public virtual ICollection<Student> Student { get; set; }
    }
}
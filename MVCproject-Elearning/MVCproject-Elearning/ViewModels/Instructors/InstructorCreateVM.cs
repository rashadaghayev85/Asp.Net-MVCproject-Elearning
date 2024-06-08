﻿using MVCproject_Elearning.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCproject_Elearning.ViewModels.Instructors
{
    public class InstructorCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(20)]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Designation { get; set; }
       
        [Required]
        public IFormFile Image { get; set; }
    }
}

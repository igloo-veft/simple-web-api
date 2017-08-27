using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class Student
    {
        [Required]
        public int SSN { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

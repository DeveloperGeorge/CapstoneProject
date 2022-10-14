using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.Models
{
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }

        public int TermId { get; set; }

        public int UserId { get; set; }

        [MaxLength(50), NotNull]
        public string CourseName { get; set; }

        public DateTime CourseStart { get; set; }

        public DateTime CourseEnd { get; set; }

        public string Status { get; set; }

        [MaxLength(50)]
        public string CourseInstructor { get; set; }

        [MaxLength(20)]
        public string PhoneNum { get; set; }


        public string Email { get; set; }
    }
}

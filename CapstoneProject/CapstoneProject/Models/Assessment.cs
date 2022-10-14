using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.Models
{
    public class Assessment
    {
        public int CourseId { get; set; }

        public int TermId { get; set; }

        public int UserId { get; set; }

        [MaxLength(50), NotNull]
        public string AssessmentName { get; set; }

        public DateTime Start { get; set; }


        public DateTime End { get; set; }

        public string Des { get; set; }

        public string AssessmentType;


        public virtual string GetAssessmentType()
        {
            AssessmentType = "General Assessment";
            return AssessmentType;
        }
    }
}

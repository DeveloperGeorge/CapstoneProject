using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.Models
{
    [Table("Exam")]
    public class Exam :Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int ExamId { get; set; }

        public string NumQuestions { get; set; }

        public string QuestionTypes { get; set; }

        public override string GetAssessmentType()
        {
            AssessmentType = "Exam";
            return AssessmentType;
        }
    }
}

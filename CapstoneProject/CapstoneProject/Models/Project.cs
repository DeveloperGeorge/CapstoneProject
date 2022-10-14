using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.Models
{
    [Table("Project")]
    public class Project : Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int ProjectId { get; set; }

        public string ProjectType { get; set; }

        public string ParticipantNum { get; set; }


        public override string GetAssessmentType()
        {
            AssessmentType = "Project";
            return AssessmentType;
        }
    }
}

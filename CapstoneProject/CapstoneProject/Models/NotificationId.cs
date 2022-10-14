using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.Models
{
    [Table("Notifications")]
    public class NotificationId
    {
        [PrimaryKey, AutoIncrement]
        public int NotifyId { get; set; }

        public int CourseId { get; set; }
        public int TermId { get; set; }
        public int ProjectId { get; set; }
        public int ExamId { get; set; }
    }
}

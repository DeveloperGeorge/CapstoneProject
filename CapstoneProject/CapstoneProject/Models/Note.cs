using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.Models
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int NoteId { get; set; }


        public int CourseId { get; set; }

        public int TermId { get; set; }

        [MaxLength(50), NotNull]
        public string NoteName { get; set; }


        public string NoteDes { get; set; }

    }
}

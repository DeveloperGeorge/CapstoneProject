using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.Models
{
    [Table("Terms")]
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int TermId { get; set; }

        [MaxLength(50), NotNull]
        public string TermName { get; set; }

        public DateTime TermStart { get; set; }

        public DateTime TermEnd { get; set; }

        public int UserId { get; set; }

    }
}

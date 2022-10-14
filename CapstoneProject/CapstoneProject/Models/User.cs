using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.Models
{
    [Table("Users")]
    class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        [MaxLength(50), NotNull]
        public string UserName { get; set; }

        [MaxLength(50), NotNull]
        public string Password { get; set; }
    }
}

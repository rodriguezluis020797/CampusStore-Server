using System;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("UserPasswordHash")]
    public class UserPasswordHash
    {
        [Key]
        public Int64 UserPasswordHashId { get; set; }
        public String Sha256Hash { get; set; }

    }
}
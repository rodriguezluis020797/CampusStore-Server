using System;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Server.Models
{
    [Table("User")]
    public class UserModel
    {
        [Key]
        public Int64 UserId { get; set; }
        public String FirstName { get; set; }
        public String? MiddleName { get; set; }
        public String LastName { get; set; }

        public void assign(UserModel user){
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
        }
    }

    

}
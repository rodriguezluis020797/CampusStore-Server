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

using Server.BOL;


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
        public String EMail { get; set; }

        public String validateUser()
        {
            if (String.IsNullOrWhiteSpace(FirstName))
            {
                return "Valid first name required.";
            }
            else if (String.IsNullOrWhiteSpace(LastName))
            {
                return "Valid last name required.";
            }
            else if (ContactInfoBOL.IsValidEmail(EMail) == false)
            {
                return "Valid E-Mail format required.";
            }

            return null;
        }

        public void assign(UserModel user)
        {
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
            this.EMail = user.EMail;
        }
        public void createUser(UserModel user)
        {
            this.assign(user);
        }
        public void updateUser(UserModel user)
        {
            this.UserId = user.UserId;
            this.assign(user);
        }
    }

}
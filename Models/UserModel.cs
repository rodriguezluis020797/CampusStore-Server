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

using Server.Data;

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
        public bool HasLoggedIn { get; set; }

        public String validateUser()
        {
            if (ContactInfoBOL.IsValidEmail(this.EMail) == false)
            {
                return "Valid E-Mail format required.";
            }
            else if (this.EMail.Substring(this.EMail.Length - 4).Equals(".edu") == false)
            {
                return "Only accepting '.edu' E-Mail domains.";
            }
            String str;
            using (GeneralContext gc = new GeneralContext())
            {
                str = gc.Users.Where(x => x.EMail == this.EMail).Select(x => x.EMail).FirstOrDefault();
            }
            if (str != null)
            {
                return "Profile with email '" + str + "' already exists.";
            }
            else if (String.IsNullOrWhiteSpace(this.FirstName))
            {
                return "Valid first name required.";
            }
            else if (String.IsNullOrWhiteSpace(this.LastName))
            {
                return "Valid last name required.";
            }



            return null;
        }

        public void assign(UserModel user)
        {
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
            this.EMail = user.EMail;
            this.HasLoggedIn = user.HasLoggedIn;
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
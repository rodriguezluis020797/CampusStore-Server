using System;

namespace Server.Models
{
    public class NewUserModel
    {
        public UserModel user { get; set; }
        public String tempPassword { get; set; }
    }

}
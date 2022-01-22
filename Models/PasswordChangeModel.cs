using System;

namespace Server.Models{
    public class PasswordChangeModel{
        public Int64 UserId { get; set; }
        public String OldPassword { get; set; }
        public String NewPassword { get; set; }
    }
}
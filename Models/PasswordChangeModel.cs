using System;

namespace Server.Models{
    public class PasswordChangeModel{
        public Int64 UserId { get; set; }
        public String OldPassword { get; set; }
        public String NewPassword { get; set; }

        public String validate(){
            int minPassLength = 6;

            if(NewPassword.Length <= minPassLength){
                return "Password must be at least 7 characters long.";
            }
            return null;
        }
        
    }
}
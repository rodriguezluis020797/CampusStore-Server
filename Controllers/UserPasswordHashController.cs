using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using Server.Models;
using Server.Data;
using Server.BOL;

namespace Server.Controllers
{
    [Route("userpasswordhashes")]
    public class UserPasswordHashIdController : Controller
    {

        private static readonly int tempPasswordLength = 10;
        //set
        [HttpPost("[action]")]
        public IActionResult setPassword([FromBody] PasswordChangeModel passwordChangeModel)
        {
            try
            {
                String str = passwordChangeModel.validate();
                if (str != null)
                {
                    return new ObjectResult(str);
                }

                using (CredentialContext cc = new CredentialContext())
                {
                    str = cc.UserPasswordHashes
                    .Where(x => x.UserPasswordHashId == passwordChangeModel.UserId)
                    .Select(x => x.Sha256Hash).FirstOrDefault();
                }

                if (str != null)
                {
                    return new ObjectResult("Password for user already exists");
                }

                UserPasswordHash newHash = new UserPasswordHash();
                newHash.UserPasswordHashId = passwordChangeModel.UserId;
                newHash.Sha256Hash = CryptographyBOL.getSha256Hash(passwordChangeModel.NewPassword);

                using (CredentialContext cc = new CredentialContext())
                {
                    cc.UserPasswordHashes.Add(newHash);
                    cc.SaveChanges();
                }

                return new ObjectResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }

        //change
        [HttpPut("[action]")]
        public IActionResult changePassword([FromBody] PasswordChangeModel passwordChangeModel)
        {
            try
            {
                String str = passwordChangeModel.validate();
                if (str != null)
                {
                    return new ObjectResult(str);
                }
                
                //retrieve old password
                UserPasswordHash currentHash;

                using (CredentialContext cc = new CredentialContext())
                {
                    currentHash = cc.UserPasswordHashes.Where(x => x.UserPasswordHashId == passwordChangeModel.UserId).FirstOrDefault();
                }
                if (currentHash == null)
                {
                    return new StatusCodeResult(400);
                }
                //compare password hashes
                if (currentHash.Sha256Hash.Equals(CryptographyBOL.getSha256Hash(passwordChangeModel.OldPassword)) == false)
                {
                    return new ObjectResult("Incorrect password.");
                }

                //set new password hash
                using (CredentialContext cc = new CredentialContext())
                {
                    currentHash = cc.UserPasswordHashes.Where(x => x.UserPasswordHashId == passwordChangeModel.UserId).FirstOrDefault();
                    currentHash.Sha256Hash = CryptographyBOL.getSha256Hash(passwordChangeModel.NewPassword);
                    cc.Update(currentHash);
                    cc.SaveChanges();
                }

                return new ObjectResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("[action]/{email}")]
        public IActionResult resetPassword(String eMail){
            try{
                UserPasswordHash passwordHash;
                Int64 userId;
                String tempPassword;

                using (GeneralContext gc = new GeneralContext())
                {
                    userId = gc.Users.Where(x => x.EMail == eMail).Select(x => x.UserId).FirstOrDefault();
                }

                if(userId <= 0){
                    return new ObjectResult(false);
                }

                //Length 7
                
                using(CredentialContext cc = new CredentialContext()){

                    passwordHash = cc.UserPasswordHashes.Where(x => x.UserPasswordHashId == userId).FirstOrDefault();
                    CryptographyBOL cryp = new CryptographyBOL();
                    tempPassword = cryp.RandomString(UserPasswordHashIdController.tempPasswordLength);
                    passwordHash.Sha256Hash = CryptographyBOL.getSha256Hash(tempPassword);

                    cc.UserPasswordHashes.Update(passwordHash);
                    cc.SaveChanges();
                }

                EmailBOL emailBOL = new EmailBOL();
                emailBOL.setTempPassword(tempPassword, eMail, "");

                return new ObjectResult(true);
            }catch (Exception e){
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }

        /*
        Non-Routed Methods
        */

        public static String setTempPassword(Int64 userId){

            try{
                UserPasswordHash newUserPasswordHash = new UserPasswordHash();
                CryptographyBOL cryp = new CryptographyBOL();
                //Length 7
                String str = cryp.RandomString(UserPasswordHashIdController.tempPasswordLength);

                newUserPasswordHash.UserPasswordHashId = userId;
                newUserPasswordHash.Sha256Hash = CryptographyBOL.getSha256Hash(str);

                using(CredentialContext cc = new CredentialContext()){
                    cc.UserPasswordHashes.Add(newUserPasswordHash);
                    cc.SaveChanges();
                }

                return str;

            } catch (Exception e){
                Console.WriteLine(e);
                return null;
            }
        }

    }
}
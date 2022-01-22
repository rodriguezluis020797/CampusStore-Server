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

    [Route("users")]
    /*
    Basic CRUD operations
    //delete not yet implemented
    */
    public class UserController : Controller
    {
        //Get user
        [HttpPut("[action]")]
        public IActionResult getUser([FromBody] LoginModel loginInfo)
        {
            try
            {
                UserModel user;
                String passwordHash;
                //Get user id
                using (GeneralContext gc = new GeneralContext())
                {
                    user = gc.Users.Where(x => x.EMail.Equals(loginInfo.ProvidedEmail)).FirstOrDefault();
                }

                if(user == null){
                    return new ObjectResult("Invalid username or password.");
                }

                //get password hash
                using (CredentialContext cc = new CredentialContext()){
                    passwordHash = cc.UserPasswordHashes.Where(x => x.UserPasswordHashId == user.UserId).Select(x => x.Sha256Hash).FirstOrDefault();
                }
                if(passwordHash == null){
                    return new ObjectResult("Invalid username or password.");
                }

                //compare hashes
                if(passwordHash.Equals(CryptographyBOL.getSha256Hash(loginInfo.ProvidedPassword))){
                    return new ObjectResult(user);
                }

                return new ObjectResult("Invalid username or password.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }

        //create user
        [HttpPost("[action]")]
        public IActionResult createUser([FromBody] UserModel user)
        {
            String errorMessage = user.validateUser();
            if (errorMessage != null)
            {
                return new ObjectResult(errorMessage);
            }
            try
            {
                UserModel newUser = new UserModel();
                newUser.assign(user);

                using (GeneralContext gc = new GeneralContext())
                {
                    gc.Users.Add(newUser);
                    gc.SaveChanges();
                }

                return new ObjectResult(newUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }

        //update user
        [HttpPut("[action]")]
        public IActionResult updateUser([FromBody] UserModel user)
        {
            String errorMessage = user.validateUser();
            if (errorMessage != null)
            {
                return new ObjectResult(errorMessage);
            }
            try
            {
                UserModel userToUpdate;

                using (GeneralContext gc = new GeneralContext())
                {
                    userToUpdate = gc.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();

                    if (userToUpdate == null)
                    {
                        return new StatusCodeResult(400);
                    }
                    userToUpdate.updateUser(user);

                    gc.Update(userToUpdate);
                    gc.SaveChanges();
                }

                return new ObjectResult(userToUpdate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }
    }
}


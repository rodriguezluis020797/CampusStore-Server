using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using Server.Models;
using Server.Data;

namespace Server.Controllers {
    
    [Route("user")]
    public class UserController : Controller {
        [HttpGet("[action]/{userId}")]
        public IActionResult getUser(Int64 userId){

            try{

                UserModel user;

                using(GeneralContext gc = new GeneralContext()){
                    user = gc.Users.Where(x => x.UserId == userId).FirstOrDefault();
                }

                if(user == null){
                    return new StatusCodeResult(400);
                }

                return new ObjectResult(user);

            } catch(Exception e){

                Console.WriteLine(e);

                return new StatusCodeResult(500);

            }
            
        }
    }
}
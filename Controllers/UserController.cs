using System;
using Microsoft.AspNetCore.Mvc;

using Server.Models;

namespace Server.Controllers {
    
    [Route("user")]
    public class UserController : Controller {
        [HttpGet("[action]")]
        public IActionResult getUser(){
            UserModel user = new UserModel();
            return new ObjectResult(user);
        }
    }
}
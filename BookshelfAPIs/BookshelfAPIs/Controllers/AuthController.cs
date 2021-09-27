using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookshelfAPIs.Models;
using System.Web.Http.Cors;

namespace BookshelfAPIs.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        AuthDatabase db = AuthDatabase.instantiateDB();

        [HttpPost]
        [Route("Login")]
        public bool login(User user)
        {
            return db.login(user);
        }

        [HttpPost]
        [Route("Register")]
        public bool register(User user)
        {
            return db.register(user);
        }
    }
}
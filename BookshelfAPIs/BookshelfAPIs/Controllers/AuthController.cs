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
        [Route("auth/login")]
        public HttpResponseMessage login(User user)
        {
            try
            {
                User data = db.login(user);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(e);
            }
        }

        [HttpPost]
        [Route("auth/register")]
        public HttpResponseMessage register(User user)
        {
            try
            {
                bool data = db.register(user);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }
        }
    }
}
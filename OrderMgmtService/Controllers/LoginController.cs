using OrderMgmtService.Data_Access;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace OrderMgmtService.Controllers
{
    public class LoginController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();
        [ResponseType(typeof(Security_User))]
        [ActionName("LogonTheUser")]
        [HttpGet]
        public IHttpActionResult LogonTheUser(string username, string password)
        {
            Security_User security_User = OrderMgmtService.Controllers.Security_UserController.db.Security_User.First(x => (x.UserName == username && x.Password == password));
            if (security_User == null)
            {
                return NotFound();
            }
            else
            {
                Guid newToken = Guid.NewGuid();
                security_User.IsActive = true;
                security_User.ActiveToken = newToken;
                OrderMgmtService.Controllers.Security_UserController objectSecurityUser = new Security_UserController();
                Security_UserSession newUserSession = new Security_UserSession();
                newUserSession.UserId = security_User.UserId;
                newUserSession.Token = newToken;
                newUserSession.CreatedTimestamp = DateTime.Now;
                OrderMgmtService.Controllers.Security_UserSessionController objectSecurityUserSession = new Security_UserSessionController();
                objectSecurityUserSession.PostSecurity_UserSession(newUserSession);
                objectSecurityUser.PutSecurity_User(security_User.UserId, security_User);
                return Ok(newUserSession.Token.ToString());
            }
        }

        [ResponseType(typeof(void))]
        [ActionName("LogoutUserSession")]
        [HttpGet]
        public IHttpActionResult LogoutUserSession(Guid token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (testAPIEntities db = new testAPIEntities())
            {
                var result = db.Security_User.SingleOrDefault(b => b.ActiveToken == token);
                if (result != null)
                {
                    result.IsActive = false;
                    result.ActiveToken = null;
                    db.SaveChanges();
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

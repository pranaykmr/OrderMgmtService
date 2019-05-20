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
        public IHttpActionResult GetLogonTheUser(string username, string password)
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
        public IHttpActionResult PutLogoutUserSession(Guid id)
        {
            Security_User security_User = new Security_User();
            security_User.IsActive = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(security_User).Property(x => x.IsActive).IsModified = true;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

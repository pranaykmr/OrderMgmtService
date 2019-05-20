using OrderMgmtService.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace OrderMgmtService.Controllers
{
    public class LoginController : ApiController
    {

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
                security_User.IsActive = true;
                OrderMgmtService.Controllers.Security_UserController objectSecurityUser = new Security_UserController();
                objectSecurityUser.PutSecurity_User(security_User.UserId, security_User);
                Security_UserSession newUserSession = new Security_UserSession();
                newUserSession.UserId = security_User.UserId;
                newUserSession.Token = Guid.NewGuid();
                newUserSession.CreatedTimestamp = DateTime.Now;
                OrderMgmtService.Controllers.Security_UserSessionController objectSecurityUserSession = new Security_UserSessionController();
                objectSecurityUserSession.PostSecurity_UserSession(newUserSession);
                return Ok(newUserSession.Token.ToString());
            }
        }
    }
}

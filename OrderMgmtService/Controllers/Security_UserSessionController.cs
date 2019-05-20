using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OrderMgmtService.Data_Access;

namespace OrderMgmtService.Controllers
{
    public class Security_UserSessionController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();

        // GET: api/Security_UserSession
        public IQueryable<Security_UserSession> GetSecurity_UserSession()
        {
            return db.Security_UserSession;
        }

        // GET: api/Security_UserSession/5
        [ResponseType(typeof(Security_UserSession))]
        public IHttpActionResult GetSecurity_UserSessionValid(Guid id)
        {
            OrderMgmtService.Data_Access.Security_User user = db.Security_User.First(x => x.ActiveToken == id);
            if (user.ActiveToken == null)
            {
                return NotFound();
            }
            bool isValid = (db.Security_UserSession.Count(e => user.ActiveToken == id) > 0);
            if (!isValid)
            {
                return NotFound();
            }
            return Ok(true);
        }

        // PUT: api/Security_UserSession/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSecurity_UserSession(Guid id, Security_UserSession security_UserSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != security_UserSession.Token)
            {
                return BadRequest();
            }

            db.Entry(security_UserSession).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Security_UserSessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Security_UserSession
        [ResponseType(typeof(Security_UserSession))]
        public IHttpActionResult PostSecurity_UserSession(Security_UserSession security_UserSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Security_UserSession.Add(security_UserSession);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Security_UserSessionExists(security_UserSession.Token))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = security_UserSession.Token }, security_UserSession);
        }

        // DELETE: api/Security_UserSession/5
        [ResponseType(typeof(Security_UserSession))]
        public IHttpActionResult DeleteSecurity_UserSession(Guid id)
        {
            Security_UserSession security_UserSession = db.Security_UserSession.Find(id);
            if (security_UserSession == null)
            {
                return NotFound();
            }

            db.Security_UserSession.Remove(security_UserSession);
            db.SaveChanges();

            return Ok(security_UserSession);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Security_UserSessionExists(Guid id)
        {
            return db.Security_UserSession.Count(e => e.Token == id) > 0;
        }
    }
}
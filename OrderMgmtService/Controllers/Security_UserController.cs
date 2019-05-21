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
    public class Security_UserController : ApiController
    {
        public testAPIEntities db = new testAPIEntities();

        // GET: api/Security_User
        public IQueryable<Security_User> GetSecurity_User()
        {
            return db.Security_User;
        }

        // GET: api/Security_User/5
        [ResponseType(typeof(Security_User))]
        public IHttpActionResult GetSecurity_User(Guid id)
        {
            Security_User security_User = db.Security_User.Find(id);
            if (security_User == null)
            {
                return NotFound();
            }

            return Ok(security_User);
        }

        // GET: api/Security_User/5
        [ResponseType(typeof(Security_User))]
        public IHttpActionResult GetSecurity_User(String name)
        {
            Security_User security_User = db.Security_User.First(x => x.UserName == name);
            if (security_User == null)
            {
                return NotFound();
            }

            return Ok(security_User);
        }

        // PUT: api/Security_User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSecurity_User(Guid id, Security_User security_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != security_User.UserId)
            {
                return BadRequest();
            }

            db.Entry(security_User).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Security_UserExists(id))
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

        // POST: api/Security_User
        [ResponseType(typeof(Security_User))]
        public IHttpActionResult PostSecurity_User(Security_User security_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Security_User.Add(security_User);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Security_UserExists(security_User.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = security_User.UserId }, security_User);
        }

        // DELETE: api/Security_User/5
        [ResponseType(typeof(Security_User))]
        public IHttpActionResult DeleteSecurity_User(Guid id)
        {
            Security_User security_User = db.Security_User.Find(id);
            if (security_User == null)
            {
                return NotFound();
            }

            db.Security_User.Remove(security_User);
            db.SaveChanges();

            return Ok(security_User);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Security_UserExists(Guid id)
        {
            return db.Security_User.Count(e => e.UserId == id) > 0;
        }
    }
}
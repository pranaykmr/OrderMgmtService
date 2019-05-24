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
    public class ShippedByController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();

        // GET: api/ShippedBy
        public IQueryable<ShippedBy> GetShippedBies()
        {
            return db.ShippedBies;
        }

        // GET: api/ShippedBy/5
        [ResponseType(typeof(ShippedBy))]
        public IHttpActionResult GetShippedBy(Guid id)
        {
            ShippedBy shippedBy = db.ShippedBies.Find(id);
            if (shippedBy == null)
            {
                return NotFound();
            }

            return Ok(shippedBy);
        }

        // PUT: api/ShippedBy/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShippedBy(Guid id, ShippedBy shippedBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shippedBy.ShipperId)
            {
                return BadRequest();
            }

            db.Entry(shippedBy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippedByExists(id))
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

        // POST: api/ShippedBy
        [ResponseType(typeof(ShippedBy))]
        public IHttpActionResult PostShippedBy(ShippedBy shippedBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShippedBies.Add(shippedBy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ShippedByExists(shippedBy.ShipperId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shippedBy.ShipperId }, shippedBy);
        }

        // DELETE: api/ShippedBy/5
        [ResponseType(typeof(ShippedBy))]
        public IHttpActionResult DeleteShippedBy(Guid id)
        {
            ShippedBy shippedBy = db.ShippedBies.Find(id);
            if (shippedBy == null)
            {
                return NotFound();
            }

            db.ShippedBies.Remove(shippedBy);
            db.SaveChanges();

            return Ok(shippedBy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShippedByExists(Guid id)
        {
            return db.ShippedBies.Count(e => e.ShipperId == id) > 0;
        }
    }
}
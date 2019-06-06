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
    public class ShippingModesController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();

        // GET: api/ShippingModes
        public IQueryable<ShippingMode> GetShippingModes()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.ShippingModes;
        }

        // GET: api/ShippingModes/5
        [ResponseType(typeof(ShippingMode))]
        public IHttpActionResult GetShippingMode(Guid id)
        {
            ShippingMode shippingMode = db.ShippingModes.Find(id);
            if (shippingMode == null)
            {
                return NotFound();
            }

            return Ok(shippingMode);
        }

        // PUT: api/ShippingModes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShippingMode(Guid id, ShippingMode shippingMode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shippingMode.ShippingMode_Id)
            {
                return BadRequest();
            }

            db.Entry(shippingMode).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingModeExists(id))
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

        // POST: api/ShippingModes
        [ResponseType(typeof(ShippingMode))]
        public IHttpActionResult PostShippingMode(ShippingMode shippingMode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShippingModes.Add(shippingMode);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ShippingModeExists(shippingMode.ShippingMode_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shippingMode.ShippingMode_Id }, shippingMode);
        }

        // DELETE: api/ShippingModes/5
        [ResponseType(typeof(ShippingMode))]
        public IHttpActionResult DeleteShippingMode(Guid id)
        {
            ShippingMode shippingMode = db.ShippingModes.Find(id);
            if (shippingMode == null)
            {
                return NotFound();
            }

            db.ShippingModes.Remove(shippingMode);
            db.SaveChanges();

            return Ok(shippingMode);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShippingModeExists(Guid id)
        {
            return db.ShippingModes.Count(e => e.ShippingMode_Id == id) > 0;
        }
    }
}
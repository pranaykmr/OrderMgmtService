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
    public class ShippingDetailsController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();

        // GET: api/ShippingDetails
        public IQueryable<ShippingDetail> GetShippingDetails()
        {
            return db.ShippingDetails;
        }

        // GET: api/ShippingDetails/5
        [ResponseType(typeof(ShippingDetail))]
        public IHttpActionResult GetShippingDetail(string id)
        {
            ShippingDetail shippingDetail = db.ShippingDetails.Find(id);
            if (shippingDetail == null)
            {
                return NotFound();
            }

            return Ok(shippingDetail);
        }

        // PUT: api/ShippingDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShippingDetail(string id, ShippingDetail shippingDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shippingDetail.Order_No)
            {
                return BadRequest();
            }

            db.Entry(shippingDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingDetailExists(id))
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

        // POST: api/ShippingDetails
        [ResponseType(typeof(ShippingDetail))]
        public IHttpActionResult PostShippingDetail(ShippingDetail shippingDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShippingDetails.Add(shippingDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ShippingDetailExists(shippingDetail.Order_No))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shippingDetail.Order_No }, shippingDetail);
        }

        // DELETE: api/ShippingDetails/5
        [ResponseType(typeof(ShippingDetail))]
        public IHttpActionResult DeleteShippingDetail(string id)
        {
            ShippingDetail shippingDetail = db.ShippingDetails.Find(id);
            if (shippingDetail == null)
            {
                return NotFound();
            }

            db.ShippingDetails.Remove(shippingDetail);
            db.SaveChanges();

            return Ok(shippingDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShippingDetailExists(string id)
        {
            return db.ShippingDetails.Count(e => e.Order_No == id) > 0;
        }
    }
}
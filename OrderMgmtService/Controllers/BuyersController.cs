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
    public class BuyersController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();

        // GET: api/Buyers
        public IQueryable<Buyer> GetBuyers()
        {
            return db.Buyers;
        }

        // GET: api/Buyers/5
        [ResponseType(typeof(Buyer))]
        public IHttpActionResult GetBuyer(Guid id)
        {
            Buyer buyer = db.Buyers.Find(id);
            if (buyer == null)
            {
                return NotFound();
            }

            return Ok(buyer);
        }

        // PUT: api/Buyers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuyer(Guid id, Buyer buyer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buyer.BuyerId)
            {
                return BadRequest();
            }

            db.Entry(buyer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyerExists(id))
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

        // POST: api/Buyers
        [ResponseType(typeof(Buyer))]
        public IHttpActionResult PostBuyer(Buyer buyer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Buyers.Add(buyer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BuyerExists(buyer.BuyerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = buyer.BuyerId }, buyer);
        }

        // DELETE: api/Buyers/5
        [ResponseType(typeof(Buyer))]
        public IHttpActionResult DeleteBuyer(Guid id)
        {
            Buyer buyer = db.Buyers.Find(id);
            if (buyer == null)
            {
                return NotFound();
            }

            db.Buyers.Remove(buyer);
            db.SaveChanges();

            return Ok(buyer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuyerExists(Guid id)
        {
            return db.Buyers.Count(e => e.BuyerId == id) > 0;
        }
    }
}
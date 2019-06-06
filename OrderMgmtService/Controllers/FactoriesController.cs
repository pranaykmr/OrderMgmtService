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
    public class FactoriesController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();

        // GET: api/Factories
        public IQueryable<Factory> GetFactories()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Factories;
        }

        // GET: api/Factories/5
        [ResponseType(typeof(Factory))]
        public IHttpActionResult GetFactory(Guid id)
        {
            Factory factory = db.Factories.Find(id);
            if (factory == null)
            {
                return NotFound();
            }

            return Ok(factory);
        }

        // PUT: api/Factories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFactory(Guid id, Factory factory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factory.Factory_Id)
            {
                return BadRequest();
            }

            db.Entry(factory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactoryExists(id))
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

        // POST: api/Factories
        [ResponseType(typeof(Factory))]
        public IHttpActionResult PostFactory(Factory factory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Factories.Add(factory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FactoryExists(factory.Factory_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = factory.Factory_Id }, factory);
        }

        // DELETE: api/Factories/5
        [ResponseType(typeof(Factory))]
        public IHttpActionResult DeleteFactory(Guid id)
        {
            Factory factory = db.Factories.Find(id);
            if (factory == null)
            {
                return NotFound();
            }

            db.Factories.Remove(factory);
            db.SaveChanges();

            return Ok(factory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FactoryExists(Guid id)
        {
            return db.Factories.Count(e => e.Factory_Id == id) > 0;
        }
    }
}
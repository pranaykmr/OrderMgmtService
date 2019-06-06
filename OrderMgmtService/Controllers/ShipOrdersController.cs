using OrderMgmtService.Data_Access;
using OrderMgmtService.Models;
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
    public class ShipOrdersController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();
        [ResponseType(typeof(bool))]
        [ActionName("SaveShippingInfo")]
        [HttpPost]
        public IHttpActionResult SaveShippingInfo(List<ShipOrder> orderinfo, ShippingDetail shippingdetails)
        {
            foreach (var item in orderinfo)
            {
                shippingdetails.Order_No = item.OrderNo;
                shippingdetails.PO_No = item.PushraseOrderNo;
                shippingdetails.Quantity = item.Quantity;
                shippingdetails.Weight = item.Weight;

                db.ShippingDetails.Add(shippingdetails);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (ShippingDetailExists(shippingdetails.Order_No))
                    {
                        //return Conflict();
                    }
                    else
                    {
                        //throw;
                    }
                }
                Order order = db.Orders.Find(item.OrderNo);
                if (order.QuantityShipped == null)
                    order.QuantityShipped = 0;
                if (item.IsFullyShipped)
                    order.Ship_Date = shippingdetails.Shipping_Date;
                else
                    order.QuantityShipped += item.Quantity;
                db.Orders.Attach(order);
                db.Entry(order).Property(x => x.Ship_Date).IsModified = true;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return Ok(true);
        }

        private bool ShippingDetailExists(string id)
        {
            return db.ShippingDetails.Count(e => e.Order_No == id) > 0;
        }
    }
}

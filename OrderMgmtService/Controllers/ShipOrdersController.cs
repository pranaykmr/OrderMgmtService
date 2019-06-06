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
        public IHttpActionResult SaveShippingInfo(AddShippingInfo data)
        {
            foreach (var item in data.orderinfo)
            {
                data.shippingdetails.Order_No = item.OrderNo;
                data.shippingdetails.PO_No = item.PushraseOrderNo;
                data.shippingdetails.Quantity = item.Quantity;

                db.ShippingDetails.Add(data.shippingdetails);


                Order order = db.Orders.Find(item.OrderNo);
                if (order.QuantityShipped == null)
                    order.QuantityShipped = 0;
                if (item.IsFullyShipped || item.Quantity >= order.Quantity)
                {
                    order.Ship_Date = data.shippingdetails.Shipping_Date;
                    order.QuantityShipped = order.Quantity;
                }
                else
                    order.QuantityShipped += item.Quantity;
                db.Entry(order).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ShippingDetailExists(data.shippingdetails.Order_No))
                    {
                        //return Conflict();
                    }
                    else
                    {
                        //throw;
                    }
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

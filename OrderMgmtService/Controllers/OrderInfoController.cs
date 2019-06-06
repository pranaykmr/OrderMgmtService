using OrderMgmtService.Data_Access;
using OrderMgmtService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace OrderMgmtService.Controllers
{
    public class OrderInfoController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();
        [ResponseType(typeof(OrderInfo[]))]
        [ActionName("GetOrderInfo")]
        [HttpGet]
        public IHttpActionResult GetOrderInfo(string token, bool getInactiveOrders)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (getInactiveOrders) { 
            var orderInfo = (from o in db.Orders
                              join f in db.Factories on o.Factory_Id equals f.Factory_Id
                              join b in db.Buyers on o.BuyerId equals b.BuyerId
                              join s in db.ShippingModes on o.ShippingMode_Id equals s.ShippingMode_Id
                              orderby o.Delivery descending
                              select new OrderInfo
                              {
                                  OrderNo = o.Order_No,
                                  StyleNo = o.Style_No,
                                  Quantity = o.Quantity,
                                  Delivery = o.Delivery,
                                  FactoryId = o.Factory_Id,
                                  FactoryName = f.Name,
                                  PushraseOrderNo = o.Purchase_Order_No,
                                  ShippingModeId = o.ShippingMode_Id,
                                  ShippingModeName = s.ShippingModeName,
                                  PriceFOB = o.Price_FOB,
                                  FactoryPrice = o.Factory_Price,
                                  TotalValue = o.Total_Value,
                                  ShipDate = o.Ship_Date,
                                  BuyerId = o.BuyerId,
                                  BuyerName = b.BuyerName,
                              });
                return Ok(orderInfo);
            }
            else
            {
                var orderInfo = (from o in db.Orders
                                 join f in db.Factories on o.Factory_Id equals f.Factory_Id
                                 join b in db.Buyers on o.BuyerId equals b.BuyerId
                                 join s in db.ShippingModes on o.ShippingMode_Id equals s.ShippingMode_Id
                                 where o.Ship_Date == null
                                 orderby o.Delivery descending
                                 select new OrderInfo
                                 {
                                     OrderNo = o.Order_No,
                                     StyleNo = o.Style_No,
                                     Quantity = o.Quantity,
                                     Delivery = o.Delivery,
                                     FactoryId = o.Factory_Id,
                                     FactoryName = f.Name,
                                     PushraseOrderNo = o.Purchase_Order_No,
                                     ShippingModeId = o.ShippingMode_Id,
                                     ShippingModeName = s.ShippingModeName,
                                     PriceFOB = o.Price_FOB,
                                     FactoryPrice = o.Factory_Price,
                                     TotalValue = o.Total_Value,
                                     ShipDate = o.Ship_Date,
                                     BuyerId = o.BuyerId,
                                     BuyerName = b.BuyerName,
                                 });
                return Ok(orderInfo);
            }
        }

    }
}

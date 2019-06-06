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
    public class ShippingandOrderDetailsController : ApiController
    {
        private testAPIEntities db = new testAPIEntities();
        [ResponseType(typeof(OrderInfo[]))]
        [ActionName("GetShippingInfo")]
        [HttpGet]
        public IHttpActionResult GetShippingInfo()
        {
            db.Configuration.LazyLoadingEnabled = false;

            var orderInfo = (from sd in db.ShippingDetails
                             join sb in db.ShippedBies on sd.ShippedById equals sb.ShipperId
                             join sm in db.ShippingModes on sd.ShippingModeId equals sm.ShippingMode_Id
                             join o in db.Orders on sd.Order_No equals o.Order_No
                             join f in db.Factories on o.Factory_Id equals f.Factory_Id
                             join b in db.Buyers on o.BuyerId equals b.BuyerId
                             orderby sd.Shipping_Date descending
                             select new ShippingOrderDetails
                             {
                                 Advised = sd.Advised,
                                 Consigned_To = sd.Consigned_To,
                                 Invoice_No = sd.Invoice_No,
                                 Order_No = sd.Order_No,
                                 PO_No = sd.PO_No,
                                 Quantity = sd.Quantity,
                                 ShippedById = sd.ShippedById,
                                 ShippingModeId = sd.ShippingModeId,
                                 Shipping_Date = sd.Shipping_Date,
                                 Weight = sd.Weight,
                                 FactoryPrice = o.Factory_Price,
                                 TotalValue = o.Total_Value,
                                 ShipDate = o.Ship_Date,
                                 BuyerId = o.BuyerId,
                                 BuyerName = b.BuyerName,
                                 ShippedByName = sb.ShipperName,
                                 ShippingModeName = sm.ShippingModeName,
                                 FactoryName = f.Name
                             });
            return Ok(orderInfo);

        }
    }
}

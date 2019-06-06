using OrderMgmtService.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderMgmtService.Models
{
    public class AddShippingInfo
    {
        public List<ShipOrder> orderinfo { get; set; }

        public ShippingDetail shippingdetails { get; set; }
    }
}
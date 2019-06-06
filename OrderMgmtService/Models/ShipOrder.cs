using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderMgmtService.Models
{
    public class ShipOrder: OrderInfo
    {
        public bool IsFullyShipped { get; set; }
        public decimal Weight { get; set; }
    }
}
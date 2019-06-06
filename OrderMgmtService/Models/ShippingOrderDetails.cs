using OrderMgmtService.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderMgmtService.Models
{
    public class ShippingOrderDetails : ShippingDetail
    {
        public decimal FactoryPrice { get; set; }
        public decimal? TotalValue { get; set; }
        public DateTime? ShipDate { get; set; }
        public System.Guid? BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string ShippingModeName { get; set; }
        public string ShippedByName { get; set; }
        public string FactoryName { get; set; }
    }
}
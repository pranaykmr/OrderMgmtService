using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderMgmtService.Models
{
    public class OrderInfo
    {
        public long OrderNo { get; set; }
        public string StyleNo { get; set; }
        public long Quantity { get; set; }
        public DateTime Delivery { get; set; }
        public System.Guid FactoryId { get; set; }
        public string FactoryName { get; set; }
        public long PushraseOrderNo { get; set; }
        public System.Guid ShippingModeId { get; set; }
        public string ShippingModeName { get; set; }
        public decimal PriceFOB { get; set; }
        public decimal FactoryPrice { get; set; }
        public decimal? TotalValue { get; set; }
        public DateTime? ShipDate { get; set; }
        public System.Guid? BuyerId { get; set; }
        public string BuyerName { get; set; }
    }
}
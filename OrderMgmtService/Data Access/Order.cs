//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrderMgmtService.Data_Access
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.ShippingDetails = new HashSet<ShippingDetail>();
        }
    
        public string Order_No { get; set; }
        public string Style_No { get; set; }
        public long Quantity { get; set; }
        public System.DateTime Delivery { get; set; }
        public System.Guid Factory_Id { get; set; }
        public string Purchase_Order_No { get; set; }
        public System.Guid ShippingMode_Id { get; set; }
        public decimal Price_FOB { get; set; }
        public decimal Factory_Price { get; set; }
        public Nullable<decimal> Total_Value { get; set; }
        public Nullable<System.DateTime> Ship_Date { get; set; }
        public Nullable<System.Guid> BuyerId { get; set; }
    
        public virtual Buyer Buyer { get; set; }
        public virtual Factory Factory { get; set; }
        public virtual ShippingMode ShippingMode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippingDetail> ShippingDetails { get; set; }
    }
}

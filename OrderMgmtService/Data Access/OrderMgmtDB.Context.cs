﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class testAPIEntities : DbContext
    {
        public testAPIEntities()
            : base("name=testAPIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Buyer> Buyers { get; set; }
        public virtual DbSet<Factory> Factories { get; set; }
        public virtual DbSet<Security_User> Security_User { get; set; }
        public virtual DbSet<ShippingMode> ShippingModes { get; set; }
        public virtual DbSet<Security_UserSession> Security_UserSession { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ShippedBy> ShippedBies { get; set; }
        public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }
    }
}

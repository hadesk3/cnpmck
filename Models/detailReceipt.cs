//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class detailReceipt
    {
        public string idReceipt { get; set; }
        public string idItem { get; set; }
        public string nameItem { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> totalPriceItem { get; set; }
    
        public virtual item item { get; set; }
        public virtual receipt receipt { get; set; }
    }
}

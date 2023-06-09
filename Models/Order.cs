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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.detailOrders = new HashSet<detailOrder>();
        }
    
        public string idOrder { get; set; }
        public string idAgency { get; set; }
        public string paymentMethod { get; set; }
        public Nullable<System.DateTime> dateOfCreation { get; set; }
        public Nullable<int> totalPrice { get; set; }
        public string orderStatus { get; set; }
        public string paymentStatus { get; set; }
    
        public virtual Agency Agency { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detailOrder> detailOrders { get; set; }
    }
}

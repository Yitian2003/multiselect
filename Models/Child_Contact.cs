//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataTrans.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Child_Contact
    {
        public int Id { get; set; }
        public Nullable<int> ChildId { get; set; }
        public Nullable<int> ContactId { get; set; }
        public Nullable<bool> CanPickup { get; set; }
        public string ActionOnAppear { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string PinCode { get; set; }
        public string Relation { get; set; }
    
        public virtual Child Child { get; set; }
        public virtual Contact Contact { get; set; }
    }
}

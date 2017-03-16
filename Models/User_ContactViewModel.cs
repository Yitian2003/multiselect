using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTrans.Models
{
    public class User_ContactViewModel
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> ContactId { get; set; }
        public Nullable<int> ContactTypeId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Lookup Lookup { get; set; }
        public virtual User User { get; set; }
    }
}
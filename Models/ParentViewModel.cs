using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTrans.Models
{
    public class ParentViewModel
    {
        public int ParentId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Parent2 { get; set; }
        public int SiteId { get; set; }
        public List<ChildViewModel> ChildList { get; set; }
    }
}
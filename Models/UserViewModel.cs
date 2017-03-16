using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTrans.Models
{
    public class UserViewModel
    {
        public string FullName { get; set; }
        public string Site { get; set; }
        public int SiteId { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }

        public bool IsStaff { get; set; }
    }
}
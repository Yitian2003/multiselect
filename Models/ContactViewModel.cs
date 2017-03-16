using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTrans.Models
{
    public class EmergencyViewModel
    {
        public int EmergencyId { get; set; }
        public string EmergencyName { get; set; }
    }
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public bool PoliceCheck { get; set; }
        public string Name { get; set; }
        public  List<EmergencyViewModel> EmergencyList {get; set;}
    }
}
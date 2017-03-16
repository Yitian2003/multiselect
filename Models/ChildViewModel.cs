using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTrans.Models
{
    public class ChildViewModel
    {
        public int ChildId { get; set; }
        public int ParentId { get; set; }
        public string ChildName { get; set; }
        //-----------------------------------
        //public int Id { get; set; }
        public Nullable<int> ContactId { get; set; }
        //public string Name { get; set; }
        public string KnownName { get; set; }
        public Nullable<int> EmergencyContact1Id { get; set; }
        public Nullable<int> EmergencyContact2Id { get; set; }
        public string Ethnicity { get; set; }
        public string LanguageSpoken { get; set; }
        public string ChildDoctorName { get; set; }
        public string ChildDoctorContact { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> GenderId { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Landline { get; set; }
        public string Mobile { get; set; }
        public string StreetNum { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> ContactTypeId { get; set; }
        public Nullable<bool> CanPickup { get; set; }
        public string ActionOnAppear { get; set; }
        public Nullable<int> ConditionId { get; set; }
        public string Severity { get; set; }
        public string DoctorName { get; set; }
        public string DoctorContact { get; set; }
        public string Symptoms { get; set; }
        public string TreatmentDesc { get; set; }
        public string MedicationDesc { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTrans.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity;

namespace DataTrans.Controllers
{
    public class HomeController : Controller
    {
        UATEntities db = new UATEntities();
        public ActionResult Index()
        {
            var siteList = db.Orgs.Where(x => x.TypeId ==6 && x.IsActive).Select(x => new { x.Id, x.Name }).ToList();
            ViewData["sites"] = siteList;

            var userList = (from user in db.Users
                            join con in db.Contacts on user.ContactId equals con.Id
                            join org_u in db.Org_User on user.Id equals org_u.UserId
                            join org in db.Orgs on org_u.OrgId equals org.Id
                            join lookup in db.Lookups on user.RoleId equals lookup.Id
                            where (org.TypeId == 6 && (user.RoleId == 8 || user.RoleId == 9) && user.IsActive)
                            select new UserViewModel
                            {
                                FullName = con.FirstName + " " + con.LastName,
                                Site = org.Name,
                                SiteId = org.Id,
                                Role = lookup.Description,
                                UserId = user.Id,
                                IsStaff = lookup.Code == "S" ? true : false
                        }).Take(1000).ToList();
            ViewData["users"] = userList;
            return View();
        }

        public ActionResult GetStaffList(int id)
        {
            using (var db = new UATEntities())
            {
                var staffHaft = (from user in db.Users
                        join con in db.Contacts on user.ContactId equals con.Id
                        where (user.Id == id && user.IsActive)
                        select new 
                        {
                            Id = user.Id,
                            staffName = con.FirstName + " " + con.MiddleName + " " + con.LastName,
                            Address = con.Address,
                            Mobile = con.Mobile,
                            PoliceCheck = con.PoliceCheck
                        }).Distinct().ToList();

                var emergency = (from user in db.Users
                                     join user_con in db.User_Contact on user.Id equals user_con.UserId
                                     join con in db.Contacts on user_con.ContactId equals con.Id
                                     where (user.Id == id && (user_con.ContactTypeId == 11 || user_con.ContactTypeId == 12)
                                     && user.IsActive)
                                     select new EmergencyViewModel
                                     {
                                         EmergencyId = user.Id,
                                         EmergencyName = con.FirstName + " " + con.LastName,
                                     }).ToList();

                var staff = (from st in staffHaft
                             join em in emergency on st.Id equals em.EmergencyId
                             select new ContactViewModel()
                             {
                                 Name = st.staffName,
                                 Address = st.Address,
                                 Mobile = st.Mobile,
                                 PoliceCheck = st.PoliceCheck,
                                 EmergencyList = emergency,
                             }).ToList();

                ViewData["staffs"] = staff;
                return View();
            }
        }

        public ActionResult GetParentList(int id, int siteId)
        {
            using (var db = new UATEntities())
            {

                var parent = (from user in db.Users
                              join con in db.Contacts on user.ContactId equals con.Id
                              join lookup in db.Lookups on user.RoleId equals lookup.Id
                              where (user.Id == id && lookup.Id == 9 && user.IsActive)
                              select new
                              {
                                  Id = user.Id,
                                  pName = con.FirstName + " " + con.LastName,
                                  Addres = con.Address,
                               }).ToList();

                var secondparent = (from user in db.Users
                                    join user_con in db.User_Contact on user.Id equals user_con.UserId
                                    join con in db.Contacts on user_con.ContactId equals con.Id
                                    where (user.Id == id && user_con.ContactTypeId == 10 && user.IsActive)
                                    select new
                                    {
                                        Id = user.Id,
                                        Name = (con.FirstName + " " + con.LastName),
                                        Address = string.Empty,
                                    }).ToList();

                var childList = (from user in db.Users
                                 join user_child in db.User_Child on user.Id equals user_child.UserId
                                 join child in db.Children on user_child.ChildId equals child.Id
                                 where (user.Id == id && user.IsActive)
                                 select new ChildViewModel()
                                 {
                                     ParentId = user.Id,
                                     ChildId = child.Id,
                                     ChildName = child.Name,
                                 }).ToList();

                if (secondparent.Count == 0 || childList.Count == 0)
                {
                    var parentList = (from p1 in parent
                                     select new ParentViewModel()
                                     {
                                         ParentId = p1.Id,
                                         FullName = p1.pName,
                                         Address = p1.Addres,
                                         Parent2 = string.Empty,
                                         ChildList = new List<ChildViewModel>()
                                         {
                                             new ChildViewModel()
                                             {
                                                 ParentId = id,
                                                 ChildId = 0,
                                                 ChildName = string.Empty,
                                             }
                                         },
                                         SiteId = siteId,
                                     }).FirstOrDefault();
                    return View(parentList);
                }
                else
                {
                    var parentList = (from p1 in parent
                                      join p2 in secondparent on p1.Id equals p2.Id
                                      select new ParentViewModel()
                                      {
                                          FullName = p1.pName,
                                          Parent2 = p2.Name,
                                          ParentId = p1.Id,
                                          Address = p1.Addres,
                                          ChildList = childList,
                                          SiteId = siteId,
                                      }).FirstOrDefault();
                    return View(parentList);
                }
                    
                
            }
        }

        public ActionResult GetChildList(int id)
        {
            //var db = new UATEntities();
            var child = db.Children.Include(x => x.Contact).Include(x => x.Child_Condition).Where(x => x.Id == id).Include(x => x.Child_Contact).Where(x => x.Id == id)
                        .Select(x => new
                        {
                            ChildId = x.Id,
                            ChildName = x.Name,
                            KnownName = x.KnownName,
                            Ethnicity = x.Ethnicity,
                            LanguageSpoken = x.LanguageSpoken,
                            EmergencyContact1Id = x.EmergencyContact1Id,
                            EmergencyContact2Id = x.EmergencyContact2Id,
                            DateOfBirth = x.Contact.DateOfBirth,
                            MedicationDesc = x.Child_Condition.Select(c => c.MedicationDesc).FirstOrDefault(),
                            TreatmentDesc = x.Child_Condition.Select(c => c.TreatmentDesc).FirstOrDefault(),
                            Symptoms = x.Child_Condition.Select(c => c.Symptoms).FirstOrDefault(),
                            Severity = x.Child_Condition.Select(c => c.Severity).FirstOrDefault(),
                            DoctorName = x.Child_Condition.Select(c => c.DoctorName).FirstOrDefault(),
                            DoctorContact = x.Child_Condition.Select(c => c.DoctorContact).FirstOrDefault(),
                            CanPickUp = x.Child_Contact.Select(c => c.CanPickup).FirstOrDefault(),
                            ActionOnAppear = x.Child_Contact.Select(c => c.ActionOnAppear).FirstOrDefault()
                        }).ToList();

            var parents = (from uc in db.User_Child
                           join u in db.Users on uc.UserId equals u.Id
                           join c in db.Contacts on u.ContactId equals c.Id
                           where uc.ChildId == id
                           select new
                           {
                               ChildId = id,
                               ParentId = uc.UserId,
                               FirstName = c.FirstName,
                               LastName = c.LastName,
                               Landline = c.Landline,
                               Mobile = c.Mobile,
                               StreetNum = c.StreetNum,
                               Address = c.Address,
                               Suburb = c.Suburb,
                               City = c.City,
                               Postcode = c.Postcode,
                               Country = c.Country
                           }).ToList();

            var details = (from a in child
                           join b in parents on a.ChildId equals b.ChildId
                           select new ChildViewModel
                           {
                               ChildName = a.ChildName,
                               KnownName = a.KnownName,
                               Ethnicity = a.Ethnicity,
                               LanguageSpoken = a.LanguageSpoken,
                               EmergencyContact1Id = a.EmergencyContact1Id,
                               EmergencyContact2Id = a.EmergencyContact2Id,
                               DateOfBirth = a.DateOfBirth,
                               MedicationDesc = a.MedicationDesc,
                               TreatmentDesc = a.TreatmentDesc,
                               Symptoms = a.Symptoms,
                               Severity = a.Severity,
                               DoctorName = a.DoctorName,
                               DoctorContact = a.DoctorContact,
                               FirstName = b.FirstName,
                               LastName = b.LastName,
                               Landline = b.Landline,
                               Mobile = b.Mobile,
                               StreetNum = b.StreetNum,
                               Address = b.Address,
                               Suburb = b.Suburb,
                               City = b.City,
                               Postcode = b.Postcode,
                               Country = b.Country,
                               CanPickup = a.CanPickUp,
                               ActionOnAppear = a.ActionOnAppear
                           }).FirstOrDefault();

            return View(details);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
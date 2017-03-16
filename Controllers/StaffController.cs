using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTrans.Models;

namespace DataTrans.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult GetStaffList(int id)
        {
            using (var db = new UATEntities())
            {
                List<ContactViewModel> list = new List<ContactViewModel>();
                list = (from user in db.Users
                        join user_con in db.User_Contact on user.Id equals user_con.UserId
                        join con in db.Contacts on user_con.ContactId equals con.Id
                        where user.Id == id
                        select new ContactViewModel
                        {
                            Name = con.FirstName + "" + con.MiddleName + "" + con.LastName,
                            Address = con.Address,
                            Mobile = con.Mobile,
                            PoliceCheck = con.PoliceCheck
                        }).ToList();
                return View(list);
            }
        }
    }
}
            
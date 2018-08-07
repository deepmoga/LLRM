using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LLRM.Areas.Auth.Models;
using Admin2.Models;
using System.Web.Mail;

namespace LLRM.Controllers
{
    public class HomeController : Controller
    {
        dbcontext db = new dbcontext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult StudentLife()
        {
            return View();
        }
        public ActionResult ChairmanMessage()
        {
            return View();
        }
        public ActionResult PrincipalMessage()
        {
            return View();
        }
        public ActionResult DirectorMessage()
        {
            return View();
        }
        // GET: Home/Details/5
        public ActionResult Departments(int id)
        {
            departmentdata dep = db.departmentdatas.Where(x => x.Depid == id).FirstOrDefault();
            TempData["a"] = id;
            return View(dep);
        }
        public ActionResult Facility(int id)
        {
            infrastructureData inf = db.infrastructureDatas.Where(x => x.Infraid == id).FirstOrDefault();
            TempData["a"] = id;
            return View(inf);
        }
       
        public ActionResult Contact()
        {
            Contact cc = db.Contacts.ToList().FirstOrDefault();
            return View(cc);
        }
        public ActionResult MissionVision()
        {
           
            return View();
        }
        public ActionResult ReadmissionCommittee()
        {
            return View();
        }
        public ActionResult ScholarshipCommittee()
        {
            return View();
        }
        public ActionResult Institute()
        {
            return View();
        }
        public ActionResult placementcell()
        {
            return View();
        }
        public ActionResult AntiRagging()
        {
            return View();
        }
        public ActionResult Courses(int id)
        {
            var cid = db.Categories.Where(x => x.catid == id).FirstOrDefault();
            TempData["Name"] = cid.Name;
           
            return View(db.Courses.Where(x => x.Catid == id).ToList());
        }
        // GET: Home/Create
        public ActionResult Albums()
        {
            return View(db.Albums.ToList());
        }
        public ActionResult Gallery()
        {
            return View(db.Galleries.ToList());
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult FullNews(int id)
        {
            News nn = db.News.Where(x => x.id == id).FirstOrDefault();
            return View(nn);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Email(string name, string phone, string email, string subject, string message)
        {


            System.Web.Mail.MailMessage Msg = new System.Web.Mail.MailMessage();
            // Sender e-mail address.
            Msg.From = email;
            // Recipient e-mail address.
            Msg.To = "enquiry@llrm.org";
            Msg.Subject = subject;
            Msg.Body = message;
            // your remote SMTP server IP.
            SmtpMail.SmtpServer = "198.38.83.183";//your ip address
            SmtpMail.Send(Msg);
            Msg = null;

            return RedirectToAction("Contact");


        }
    }
}

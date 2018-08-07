using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin2.Models;
using LLRM.Areas.Auth.Models;
using onlineportal.Areas.AdminPanel.Models;

namespace LLRM.Areas.Auth.Controllers
{
    public class AnnouncementsController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: Auth/Announcements
        public async Task<ActionResult> Index()
        {
            return View(await db.Announcements.ToListAsync());
        }

        // GET: Auth/Announcements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = await db.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // GET: Auth/Announcements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Title,Date,Description,Image")] Announcement announcement, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                announcement.Image = Help.Resize(file,400, 400);
                db.Announcements.Add(announcement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(announcement);
        }

        // GET: Auth/Announcements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = await db.Announcements.FindAsync(id);
            img = announcement.Image;
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Auth/Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Title,Date,Description,Image")] Announcement announcement, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                announcement.Image = file != null ? Help.Resize(file, 400, 400) : img;
                db.Entry(announcement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        // GET: Auth/Announcements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = await db.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Auth/Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Announcement announcement = await db.Announcements.FindAsync(id);
            db.Announcements.Remove(announcement);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

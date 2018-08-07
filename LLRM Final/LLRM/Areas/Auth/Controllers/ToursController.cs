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
    public class ToursController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: Auth/Tours
        public async Task<ActionResult> Index()
        {
            return View(await db.Tours.ToListAsync());
        }

        // GET: Auth/Tours/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = await db.Tours.FindAsync(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Auth/Tours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Title,Url,Image")] Tour tour, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                tour.Image = Help.uploadfile(file);
                db.Tours.Add(tour);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tour);
        }

        // GET: Auth/Tours/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = await db.Tours.FindAsync(id);
            img = tour.Image;
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Auth/Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Title,Url,Image")] Tour tour, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                tour.Image = file != null ? Help.uploadfile(file) : img;
                db.Entry(tour).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tour);
        }

        // GET: Auth/Tours/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = await db.Tours.FindAsync(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Auth/Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tour tour = await db.Tours.FindAsync(id);
            db.Tours.Remove(tour);
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

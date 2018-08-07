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

namespace LLRM.Areas.Auth.Controllers
{
    public class CourseGalleriesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Auth/CourseGalleries
        public async Task<ActionResult> Index()
        {
            var courseGalleries = db.CourseGalleries.Include(c => c.Courses);
            return View(await courseGalleries.ToListAsync());
        }

        // GET: Auth/CourseGalleries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGallery courseGallery = await db.CourseGalleries.FindAsync(id);
            if (courseGallery == null)
            {
                return HttpNotFound();
            }
            return View(courseGallery);
        }

        // GET: Auth/CourseGalleries/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name");
            return View();
        }

        // POST: Auth/CourseGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseGalId,CourseId,Image")] CourseGallery courseGallery)
        {
            if (ModelState.IsValid)
            {
                db.CourseGalleries.Add(courseGallery);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", courseGallery.CourseId);
            return View(courseGallery);
        }

        // GET: Auth/CourseGalleries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGallery courseGallery = await db.CourseGalleries.FindAsync(id);
            if (courseGallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", courseGallery.CourseId);
            return View(courseGallery);
        }

        // POST: Auth/CourseGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseGalId,CourseId,Image")] CourseGallery courseGallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseGallery).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", courseGallery.CourseId);
            return View(courseGallery);
        }

        // GET: Auth/CourseGalleries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGallery courseGallery = await db.CourseGalleries.FindAsync(id);
            if (courseGallery == null)
            {
                return HttpNotFound();
            }
            return View(courseGallery);
        }

        // POST: Auth/CourseGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseGallery courseGallery = await db.CourseGalleries.FindAsync(id);
            db.CourseGalleries.Remove(courseGallery);
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

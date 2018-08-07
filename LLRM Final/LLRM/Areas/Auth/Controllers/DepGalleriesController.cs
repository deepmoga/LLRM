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
    public class DepGalleriesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: Auth/DepGalleries
        public async Task<ActionResult> Index()
        {
            var depGalleries = db.DepGalleries.Include(d => d.DepartmentDatas);
            return View(await depGalleries.ToListAsync());
        }

        // GET: Auth/DepGalleries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepGallery depGallery = await db.DepGalleries.FindAsync(id);
            if (depGallery == null)
            {
                return HttpNotFound();
            }
            return View(depGallery);
        }

        // GET: Auth/DepGalleries/Create
        public ActionResult Create()
        {
            ViewBag.Depid = new SelectList(db.departmentdatas, "Depid", "name");
            return View();
        }

        // POST: Auth/DepGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepGalId,Depid,Image")] DepGallery depGallery,IEnumerable<HttpPostedFileBase> file,Helper help)
        {
            if (ModelState.IsValid)
            {
                foreach (var a in file)
                {
                    depGallery.Image = help.Resize(a, 400, 1200);
                db.DepGalleries.Add(depGallery);
                await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            ViewBag.Depid = new SelectList(db.departmentdatas, "Depid", "name", depGallery.Depid);
            return View(depGallery);
        }

        // GET: Auth/DepGalleries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepGallery depGallery = await db.DepGalleries.FindAsync(id);
            img = depGallery.Image;
            if (depGallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.Depid = new SelectList(db.departmentdatas, "Depid", "name", depGallery.Depid);
            return View(depGallery);
        }

        // POST: Auth/DepGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DepGalId,Depid,Image")] DepGallery depGallery,HttpPostedFileBase file,Helper help)
        {
            if (ModelState.IsValid)
            {
                depGallery.Image = file != null ? help.Resize(file, 400, 1200) : img;
                db.Entry(depGallery).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Depid = new SelectList(db.departmentdatas, "Depid", "name", depGallery.Depid);
            return View(depGallery);
        }

        // GET: Auth/DepGalleries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepGallery depGallery = await db.DepGalleries.FindAsync(id);
            if (depGallery == null)
            {
                return HttpNotFound();
            }
            return View(depGallery);
        }

        // POST: Auth/DepGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DepGallery depGallery = await db.DepGalleries.FindAsync(id);
            db.DepGalleries.Remove(depGallery);
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

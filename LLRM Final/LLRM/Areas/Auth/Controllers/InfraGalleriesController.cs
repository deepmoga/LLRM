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
    public class InfraGalleriesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: Auth/InfraGalleries
        public async Task<ActionResult> Index()
        {
            var infraGalleries = db.InfraGalleries.Include(i => i.Infrastructures);
            return View(await infraGalleries.ToListAsync());
        }

        // GET: Auth/InfraGalleries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfraGallery infraGallery = await db.InfraGalleries.FindAsync(id);
            if (infraGallery == null)
            {
                return HttpNotFound();
            }
            return View(infraGallery);
        }

        // GET: Auth/InfraGalleries/Create
        public ActionResult Create()
        {
            ViewBag.Infraid = new SelectList(db.infrastructureDatas, "Infraid", "Name");
            return View();
        }

        // POST: Auth/InfraGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "InfGalId,Infraid,Image")] InfraGallery infraGallery, IEnumerable<HttpPostedFileBase> file , Helper help)
        {
            if (ModelState.IsValid)
            {
                foreach (var a in file)
                {
                    infraGallery.Image = help.Resize(a, 400, 1200);
                    db.InfraGalleries.Add(infraGallery);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            ViewBag.Infraid = new SelectList(db.infrastructureDatas, "Infraid", "Name", infraGallery.Infraid);
            return View(infraGallery);
        }

        // GET: Auth/InfraGalleries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfraGallery infraGallery = await db.InfraGalleries.FindAsync(id);
            img = infraGallery.Image;
            if (infraGallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.Infraid = new SelectList(db.infrastructureDatas, "Infraid", "Name", infraGallery.Infraid);
            return View(infraGallery);
        }

        // POST: Auth/InfraGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "InfGalId,Infraid,Image")] InfraGallery infraGallery, HttpPostedFileBase file , Helper help)
        {
            if (ModelState.IsValid)
            {
                infraGallery.Image = file != null ? help.Resize(file, 400, 1200) : img ;
                db.Entry(infraGallery).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Infraid = new SelectList(db.infrastructureDatas, "Infraid", "Name", infraGallery.Infraid);
            return View(infraGallery);
        }

        // GET: Auth/InfraGalleries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfraGallery infraGallery = await db.InfraGalleries.FindAsync(id);
            if (infraGallery == null)
            {
                return HttpNotFound();
            }
            return View(infraGallery);
        }

        // POST: Auth/InfraGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            InfraGallery infraGallery = await db.InfraGalleries.FindAsync(id);
            db.InfraGalleries.Remove(infraGallery);
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

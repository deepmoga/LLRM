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
    public class CampuslivesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: Auth/Campuslives
        public async Task<ActionResult> Index()
        {
            return View(await db.Campuslives.ToListAsync());
        }

        // GET: Auth/Campuslives/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campuslife campuslife = await db.Campuslives.FindAsync(id);
            if (campuslife == null)
            {
                return HttpNotFound();
            }
            return View(campuslife);
        }

        // GET: Auth/Campuslives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Campuslives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Image")] Campuslife campuslife,IEnumerable<HttpPostedFileBase> file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                foreach (var a in file)
                {
                    campuslife.Image = Help.Resize(a, 600, 900);

                    db.Campuslives.Add(campuslife);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            return View(campuslife);
        }

        // GET: Auth/Campuslives/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campuslife campuslife = await db.Campuslives.FindAsync(id);
            img = campuslife.Image;
            if (campuslife == null)
            {
                return HttpNotFound();
            }
            return View(campuslife);
        }

        // POST: Auth/Campuslives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Image")] Campuslife campuslife, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                campuslife.Image = file != null ? Help.Resize(file, 600, 900) : img;
                db.Entry(campuslife).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(campuslife);
        }

        // GET: Auth/Campuslives/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campuslife campuslife = await db.Campuslives.FindAsync(id);
            if (campuslife == null)
            {
                return HttpNotFound();
            }
            return View(campuslife);
        }

        // POST: Auth/Campuslives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Campuslife campuslife = await db.Campuslives.FindAsync(id);
            db.Campuslives.Remove(campuslife);
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

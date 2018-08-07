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
    public class infrastructuresController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Auth/infrastructures
        public async Task<ActionResult> Index()
        {
            return View(await db.infrastructures.ToListAsync());
        }

        // GET: Auth/infrastructures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infrastructure infrastructure = await db.infrastructures.FindAsync(id);
            if (infrastructure == null)
            {
                return HttpNotFound();
            }
            return View(infrastructure);
        }

        // GET: Auth/infrastructures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/infrastructures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Name,Description,Keyword,MetaDescription")] infrastructure infrastructure)
        {
            if (ModelState.IsValid)
            {
                db.infrastructures.Add(infrastructure);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(infrastructure);
        }

        // GET: Auth/infrastructures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infrastructure infrastructure = await db.infrastructures.FindAsync(id);
            if (infrastructure == null)
            {
                return HttpNotFound();
            }
            return View(infrastructure);
        }

        // POST: Auth/infrastructures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Name,Description,Keyword,MetaDescription")] infrastructure infrastructure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(infrastructure).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(infrastructure);
        }

        // GET: Auth/infrastructures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infrastructure infrastructure = await db.infrastructures.FindAsync(id);
            if (infrastructure == null)
            {
                return HttpNotFound();
            }
            return View(infrastructure);
        }

        // POST: Auth/infrastructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            infrastructure infrastructure = await db.infrastructures.FindAsync(id);
            db.infrastructures.Remove(infrastructure);
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

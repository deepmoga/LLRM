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
    public class infrastructureDatasController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Auth/infrastructureDatas
        public async Task<ActionResult> Index()
        {
            return View(await db.infrastructureDatas.ToListAsync());
        }

        // GET: Auth/infrastructureDatas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infrastructureData infrastructureData = await db.infrastructureDatas.FindAsync(id);
            if (infrastructureData == null)
            {
                return HttpNotFound();
            }
            return View(infrastructureData);
        }

        // GET: Auth/infrastructureDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/infrastructureDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Infraid,Name,Description,Keyword,MetaDescription")] infrastructureData infrastructureData)
        {
            if (ModelState.IsValid)
            {
                db.infrastructureDatas.Add(infrastructureData);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(infrastructureData);
        }

        // GET: Auth/infrastructureDatas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infrastructureData infrastructureData = await db.infrastructureDatas.FindAsync(id);
            if (infrastructureData == null)
            {
                return HttpNotFound();
            }
            return View(infrastructureData);
        }

        // POST: Auth/infrastructureDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Infraid,Name,Description,Keyword,MetaDescription")] infrastructureData infrastructureData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(infrastructureData).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(infrastructureData);
        }

        // GET: Auth/infrastructureDatas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infrastructureData infrastructureData = await db.infrastructureDatas.FindAsync(id);
            if (infrastructureData == null)
            {
                return HttpNotFound();
            }
            return View(infrastructureData);
        }

        // POST: Auth/infrastructureDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            infrastructureData infrastructureData = await db.infrastructureDatas.FindAsync(id);
            db.infrastructureDatas.Remove(infrastructureData);
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

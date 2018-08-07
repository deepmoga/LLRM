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
    public class departmentdatasController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Auth/departmentdatas
        public async Task<ActionResult> Index()
        {
            return View(await db.departmentdatas.ToListAsync());
        }

        // GET: Auth/departmentdatas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            departmentdata departmentdata = await db.departmentdatas.FindAsync(id);
            if (departmentdata == null)
            {
                return HttpNotFound();
            }
            return View(departmentdata);
        }

        // GET: Auth/departmentdatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/departmentdatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Depid,name,Description,Keyword,MetaDescription")] departmentdata departmentdata)
        {
            if (ModelState.IsValid)
            {
                db.departmentdatas.Add(departmentdata);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(departmentdata);
        }

        // GET: Auth/departmentdatas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            departmentdata departmentdata = await db.departmentdatas.FindAsync(id);
            if (departmentdata == null)
            {
                return HttpNotFound();
            }
            return View(departmentdata);
        }

        // POST: Auth/departmentdatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Depid,name,Description,Keyword,MetaDescription")] departmentdata departmentdata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmentdata).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(departmentdata);
        }

        // GET: Auth/departmentdatas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            departmentdata departmentdata = await db.departmentdatas.FindAsync(id);
            if (departmentdata == null)
            {
                return HttpNotFound();
            }
            return View(departmentdata);
        }

        // POST: Auth/departmentdatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            departmentdata departmentdata = await db.departmentdatas.FindAsync(id);
            db.departmentdatas.Remove(departmentdata);
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

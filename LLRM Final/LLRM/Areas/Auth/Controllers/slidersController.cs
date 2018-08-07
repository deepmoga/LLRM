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
    public class slidersController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: Auth/sliders
        public async Task<ActionResult> Index()
        {
            return View(await db.sliders.ToListAsync());
        }

        // GET: Auth/sliders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slider slider = await db.sliders.FindAsync(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Auth/sliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Name,Description,Image")] slider slider, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                slider.Image = Help.uploadfile(file);
                db.sliders.Add(slider);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Auth/sliders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slider slider = await db.sliders.FindAsync(id);
            img = slider.Image; 
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Auth/sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Name,Description,Image")] slider slider, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
               
                slider.Image = file != null ? Help.uploadfile(file) : img;
                #region delete file
                string fullPath = Request.MapPath("~/UploadedFiles/" + img);
                if (img == slider.Image)
                {
                }
                else
                {
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                } 
                #endregion

                db.Entry(slider).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Auth/sliders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slider slider = await db.sliders.FindAsync(id);
            img = slider.Image;
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Auth/sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            #region delete file
            string fullPath = Request.MapPath("~/UploadedFiles/" + img);
           
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            #endregion
            slider slider = await db.sliders.FindAsync(id);
            db.sliders.Remove(slider);
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

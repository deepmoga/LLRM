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
    public class GalleriesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img,thumb;
        // GET: Auth/Galleries
        public async Task<ActionResult> Index()
        {
            var galleries = db.Galleries.Include(g => g.Albums);
            return View(await galleries.ToListAsync());
        }

        // GET: Auth/Galleries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = await db.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Auth/Galleries/Create
        public ActionResult Create()
        {
            ViewBag.Albumid = new SelectList(db.Albums, "albumid", "AlbumName");
            return View();
        }

        // POST: Auth/Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GalleryId,Albumid,Name,Thumb,Image")] Gallery gallery,IEnumerable<HttpPostedFileBase> file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                foreach (var a in file)
                {
                    gallery.Image = Help.Resize(a,600,900);
                    gallery.Thumb = Help.Resize(a, 250, 400);
                    db.Galleries.Add(gallery);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            ViewBag.Albumid = new SelectList(db.Albums, "albumid", "AlbumName", gallery.Albumid);
            return View(gallery);
        }

        // GET: Auth/Galleries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = await db.Galleries.FindAsync(id);
            img = gallery.Image;
            thumb = gallery.Thumb;
            if (gallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.Albumid = new SelectList(db.Albums, "albumid", "AlbumName", gallery.Albumid);

            return View(gallery);
        }

        // POST: Auth/Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GalleryId,Albumid,Name,Thumb,Image")] Gallery gallery, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                gallery.Image = file != null ? Help.Resize(file,600,900) : img;
                gallery.Thumb = Help.Resize(file, 250, 400);
                #region delete file
                string fullPath = Request.MapPath("~/UploadedFiles/" + img);
                string Thumbmail = Request.MapPath("~/UploadedFiles/" + thumb);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    System.IO.File.Delete(Thumbmail);
                }
                #endregion
                db.Entry(gallery).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Albumid = new SelectList(db.Albums, "albumid", "AlbumName", gallery.Albumid);
            return View(gallery);
        }

        // GET: Auth/Galleries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = await db.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Auth/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Gallery gallery = await db.Galleries.FindAsync(id);
            db.Galleries.Remove(gallery);
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

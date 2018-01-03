using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mymoney.Models;
using Microsoft.AspNet.Identity;

namespace mymoney.Controllers
{
    [Authorize]
    public class SpendingCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SpendingCategories
        public async Task<ActionResult> Index()
        {
            string userid = User.Identity.GetUserId();
            return View(await db.SpendingCategories.Where(x => x.ApplicationUserID == userid).ToListAsync());
        }

        // GET: SpendingCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpendingCategory spendingCategory = await db.SpendingCategories.FindAsync(id);
            if (spendingCategory == null)
            {
                return HttpNotFound();
            }
            return View(spendingCategory);
        }

        // GET: SpendingCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpendingCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Description,ApplicationUserID")] SpendingCategory spendingCategory)
        {
            string userid = User.Identity.GetUserId();
            spendingCategory.ApplicationUserID = userid;
            if (ModelState.IsValid)
            {
                db.SpendingCategories.Add(spendingCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(spendingCategory);
        }

        // GET: SpendingCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpendingCategory spendingCategory = await db.SpendingCategories.FindAsync(id);
            if (spendingCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserID = User.Identity.GetUserId();
            return View(spendingCategory);
        }

        // POST: SpendingCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,ApplicationUserID")] SpendingCategory spendingCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spendingCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(spendingCategory);
        }

        // GET: SpendingCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpendingCategory spendingCategory = await db.SpendingCategories.FindAsync(id);
            if (spendingCategory == null)
            {
                return HttpNotFound();
            }
            return View(spendingCategory);
        }

        // POST: SpendingCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SpendingCategory spendingCategory = await db.SpendingCategories.FindAsync(id);
            db.SpendingCategories.Remove(spendingCategory);
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

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
    public class SpendingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Spendings
        public async Task<ActionResult> Index()
        {
            string userid = User.Identity.GetUserId();
            return View(await db.Spendings.Where(x => x.ApplicationUserID == userid).ToListAsync());
        }

        // GET: Spendings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spending spending = await db.Spendings.FindAsync(id);
            if (spending == null)
            {
                return HttpNotFound();
            }
            return View(spending);
        }

        // GET: Spendings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Spendings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Description,TransactionDate,Amount")] Spending spending)
        {
            string userid = User.Identity.GetUserId();
            spending.ApplicationUserID = userid;
            if (ModelState.IsValid)
            {
                db.Spendings.Add(spending);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(spending);
        }

        // GET: Spendings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spending spending = await db.Spendings.FindAsync(id);
            if (spending == null)
            {
                return HttpNotFound();
            }
            return View(spending);
        }

        // POST: Spendings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Description,TransactionDate,Amount,ApplicationUserID")] Spending spending)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spending).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(spending);
        }

        // GET: Spendings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spending spending = await db.Spendings.FindAsync(id);
            if (spending == null)
            {
                return HttpNotFound();
            }
            return View(spending);
        }

        // POST: Spendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Spending spending = await db.Spendings.FindAsync(id);
            db.Spendings.Remove(spending);
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

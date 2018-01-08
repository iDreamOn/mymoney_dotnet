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
using System.Web.Script.Serialization;

namespace mymoney.Controllers
{
    [Authorize]
    public class BudgetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Budgets
        public async Task<ActionResult> Index(DateTime? SearchDate)
        {
            if(SearchDate == null)
            {
                SearchDate = DateTime.Now;
            }

            string userid = User.Identity.GetUserId();
            var budgets = db.Budgets.Where(c => c.StartDate <= SearchDate && SearchDate <= c.EndDate && c.ApplicationUserID == userid)
                .Include(b => b.Category)
                .Include(b => b.Spendings);
            return View(await budgets.ToListAsync());
        }

        // GET: Budgets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = await db.Budgets.FindAsync(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            budget.Spendings = await db.Spendings.Where(b => b.BudgetID == budget.ID).OrderByDescending(s => s.TransactionDate).ToListAsync();
            return View(budget);
        }

        // GET: Budgets/Create
        public ActionResult Create()
        {
            string userid = User.Identity.GetUserId();
            ViewBag.CategoriesNames = new SelectList(db.SpendingCategories.Where(x => x.ApplicationUserID == userid), "Id", "Name");
            return View();
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ApplicationUserID,SpendingCategoryID,StartDate,EndDate,Amount")] Budget budget)
        {
            string userid = User.Identity.GetUserId();
            budget.ApplicationUserID = userid;
            if (ModelState.IsValid)
            {
                db.Budgets.Add(budget);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriesNames = new SelectList(db.SpendingCategories.Where(x => x.ApplicationUserID == userid), "Id", "Name", budget.Category.Name);
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = await db.Budgets.FindAsync(id);
            if (budget == null)
            {
                return HttpNotFound();
            }

            string userid = User.Identity.GetUserId();
            ViewBag.CategoriesNames = new SelectList(db.SpendingCategories.Where(x => x.ApplicationUserID == userid), "Id", "Name", budget.Category.Name);
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ApplicationUserID,SpendingCategoryID,StartDate,EndDate,Amount")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budget).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            string userid = User.Identity.GetUserId();
            ViewBag.CategoriesNames = new SelectList(db.SpendingCategories.Where(x => x.ApplicationUserID == userid), "Id", "Name", budget.Category.Name);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = await db.Budgets.FindAsync(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Budget budget = await db.Budgets.FindAsync(id);
            db.Budgets.Remove(budget);
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

        [HttpGet]
        public ActionResult GetByDate(DateTime TransactionDate)
        {
            string userid = User.Identity.GetUserId();
            var budgets = db.Budgets.Where(c => c.StartDate <= TransactionDate && TransactionDate <= c.EndDate && c.ApplicationUserID == userid).Include(b => b.Category);
            var query = from budget in budgets
                        select new
                        {
                            Name = budget.Category.Name,
                            ID = budget.ID
                        };
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(query);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}

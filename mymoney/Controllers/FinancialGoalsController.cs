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
using mymoney.Utilities;

namespace mymoney.Controllers
{
    [Authorize]
    public class FinancialGoalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FinancialGoals
        public async Task<ActionResult> Index()
        {
            string userid = User.Identity.GetUserId();
            var financialGoals = db.FinancialGoals.Where(f => f.ApplicationUserID == userid);
            return View(await financialGoals.ToListAsync());
        }

        // GET: FinancialGoals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialGoal financialGoal = await db.FinancialGoals.FindAsync(id);
            if (financialGoal == null)
            {
                return HttpNotFound();
            }
            ViewBag.DaysToCompletion = DateUtility.GetDaysBetweenDates(financialGoal.StartDate, financialGoal.EndDate);
            ViewBag.WeeklyPayments = financialGoal.GetWeeklyPayments();
            ViewBag.BiWeeklyPayments = financialGoal.GetBiWeeklyPayments();
            ViewBag.MonthlyPayments = financialGoal.GetMonthlyPayments();

            ViewBag.EarlyDate = financialGoal.EndDate.AddDays(-1*ViewBag.DaysToCompletion/2).Date;
            ViewBag.EarlyWeeklyPayments = financialGoal.GetModifiedWeeklyPayments(ViewBag.EarlyDate);
            ViewBag.EarlyBiWeeklyPayments = financialGoal.GetModifiedBiWeeklyPayments(ViewBag.EarlyDate);
            ViewBag.EarlyMonthlyPayments = financialGoal.GetModifiedMonthlyPayments(ViewBag.EarlyDate);

            ViewBag.EarlyDate3_4 = financialGoal.EndDate.AddDays(-1 * ViewBag.DaysToCompletion / 4).Date;
            ViewBag.EarlyWeeklyPayments3_4 = financialGoal.GetModifiedWeeklyPayments(ViewBag.EarlyDate3_4);
            ViewBag.EarlyBiWeeklyPayments3_4 = financialGoal.GetModifiedBiWeeklyPayments(ViewBag.EarlyDate3_4);
            ViewBag.EarlyMonthlyPayments3_4 = financialGoal.GetModifiedMonthlyPayments(ViewBag.EarlyDate3_4);
            return View(financialGoal);
        }

        // GET: FinancialGoals/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserID = User.Identity.GetUserId();
            return View();
        }

        // POST: FinancialGoals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ApplicationUserID,financialGoalOption,Name,StartDate,EndDate,StartingAmount,GoalAmount,MinPayment")] FinancialGoal financialGoal)
        {
            if (ModelState.IsValid)
            {
                db.FinancialGoals.Add(financialGoal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserID = User.Identity.GetUserId();
            return View(financialGoal);
        }

        // GET: FinancialGoals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialGoal financialGoal = await db.FinancialGoals.FindAsync(id);
            if (financialGoal == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserID = User.Identity.GetUserId();
            return View(financialGoal);
        }

        // POST: FinancialGoals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ApplicationUserID,financialGoalOption,Name,StartDate,EndDate,StartingAmount,GoalAmount,MinPayment")] FinancialGoal financialGoal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(financialGoal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = User.Identity.GetUserId();
            return View(financialGoal);
        }

        // GET: FinancialGoals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialGoal financialGoal = await db.FinancialGoals.FindAsync(id);
            if (financialGoal == null)
            {
                return HttpNotFound();
            }
            return View(financialGoal);
        }

        // POST: FinancialGoals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FinancialGoal financialGoal = await db.FinancialGoals.FindAsync(id);
            db.FinancialGoals.Remove(financialGoal);
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

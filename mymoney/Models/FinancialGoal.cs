using mymoney.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mymoney.Utilities;

namespace mymoney.Models
{
    public class FinancialGoal
    {
        public int ID { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        [Display(Name = "Goal")]
        public FinancialGoalOption financialGoalOption { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date), Display(Name = "From")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), Display(Name = "To")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Currency)]
        public decimal StartingAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal GoalAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal MinPayment { get; set; }

        public decimal GetWeeklyPayments()
        {
            return GetModifiedWeeklyPayments(this.EndDate);
        }
        public decimal GetBiWeeklyPayments()
        {
            return GetModifiedBiWeeklyPayments(this.EndDate);
        }
        public decimal GetMonthlyPayments()
        {
            return GetModifiedMonthlyPayments(this.EndDate);
        }

        public decimal GetModifiedWeeklyPayments(DateTime EndDate)
        {
            int v = DateUtility.GetWeeksBetweenDates(this.StartDate, EndDate);
            if (v == 0)
            {
                return 0;
            }
            return Math.Abs(Math.Round((this.StartingAmount - this.GoalAmount) / v));
        }
        public decimal GetModifiedBiWeeklyPayments(DateTime EndDate)
        {

            int biweeks = DateUtility.GetBiWeeksBetweenDates(this.StartDate, EndDate);
            if (biweeks == 0)
            {
                return 0;
            }
            return Math.Abs(Math.Round((this.StartingAmount - this.GoalAmount) / biweeks));
        }
        public decimal GetModifiedMonthlyPayments(DateTime EndDate)
        {
            int months = DateUtility.GetMonthsBetweenDates(this.StartDate, EndDate);
            if (months == 0)
            {
                return 0;
            }
            return Math.Abs(Math.Round((this.StartingAmount - this.GoalAmount) / months));
        }
    }
}
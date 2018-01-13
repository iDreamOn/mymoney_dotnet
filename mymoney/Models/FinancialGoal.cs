using mymoney.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}
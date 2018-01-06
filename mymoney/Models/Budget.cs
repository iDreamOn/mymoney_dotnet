using System;
using System.ComponentModel.DataAnnotations;

namespace mymoney.Models
{
    public class Budget
    {
        public int ID { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public int SpendingCategoryID { get; set; }
        public virtual SpendingCategory Category { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
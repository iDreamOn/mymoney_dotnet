using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
        public ICollection<Spending> Spendings { get; set; }

        public override string ToString()
        {
            return String.Format("{0} ({1} - {2})",this.Category.Name, this.StartDate, this.EndDate);
        }
    }
}
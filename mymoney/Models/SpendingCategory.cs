using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mymoney.Models
{
    public class SpendingCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}
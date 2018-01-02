using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mymoney.Models
{
    public class Spending
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}
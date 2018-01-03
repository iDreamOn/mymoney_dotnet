using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mymoney.Models
{
    public class Spending
    {
        public int ID { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}
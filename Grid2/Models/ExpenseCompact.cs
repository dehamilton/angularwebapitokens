using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserExpense.Models
{
    public class ExpenseCompact
    {
        public int ExpenseId { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal? Amount { get; set; }
    }
}
namespace UserExpense.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Expense")]
    public partial class Expense
    {
        public int ExpenseId { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? ExpenseDate { get; set; }

        [StringLength(40)]
        public string Description { get; set; }

        [StringLength(20)]
        public string Category { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? SubmittedDate { get; set; }
    }
}

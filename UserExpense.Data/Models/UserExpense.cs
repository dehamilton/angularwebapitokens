namespace UserExpense.Data.Models
{
    using System.Data.Entity;

    public partial class UserExpenseContext : DbContext
    {
        public UserExpenseContext()
            : base("name=UserExpense")
        {
        }

        public virtual DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Category)
                .IsUnicode(false);
        }
    }
}

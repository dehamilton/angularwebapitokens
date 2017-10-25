using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using UserExpense.Data.Models;
using UserExpense.Models;
using UserExpense.WebApi.Controllers;

namespace UserExpense.Controllers
{
    [Authorize]
    public class ExpensesController : BaseApiController
    {
        private readonly UserExpenseContext _dbContext = new UserExpenseContext();

        // GET: api/Expenses
        public IQueryable<ExpenseCompact> GetExpenses()
        {
            var userId = Guid.Parse(User.Identity.GetUserId()); // Guid.Parse not linq-able
            return _dbContext.Expenses.Where(e => e.UserId == userId).AsCompact().OrderByDescending(x => x.ExpenseDate);
        }

        // GET: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult GetExpense(int id)
        {
            var expense = _dbContext.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        // PUT: api/Expenses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExpense(int id, Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            _dbContext.Entry(expense).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Expenses
        [ResponseType(typeof(Expense))]
        public IHttpActionResult PostExpense(Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            expense.UserId = Guid.Parse(User.Identity.GetUserId());
            expense.SubmittedDate = DateTime.UtcNow;
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = expense.ExpenseId }, expense);
        }

        // DELETE: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult DeleteExpense(int id)
        {
            Expense expense = _dbContext.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            _dbContext.Expenses.Remove(expense);
            _dbContext.SaveChanges();

            return Ok(expense);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExpenseExists(int id)
        {
            return _dbContext.Expenses.Count(e => e.ExpenseId == id) > 0;
        }
    }

    public static class ExpenseExtensions
    {
        // Example to take advantage of IQueryable
        public static IQueryable<ExpenseCompact> AsCompact(this IQueryable<Expense> expenses)
        {
            return from e in expenses
                   where e.ExpenseDate.HasValue
                select new ExpenseCompact
                {
                    ExpenseId = e.ExpenseId,
                    Description = e.Description,
                    Category = e.Category,
                    Amount = e.Amount,
                    ExpenseDate = e.ExpenseDate
                };
        }
    }
}
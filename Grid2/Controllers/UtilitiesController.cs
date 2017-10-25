using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UserExpense.Data.Models;
using UserExpense.WebApi.Controllers;

namespace UserExpense.Controllers
{
    [Authorize]
    [RoutePrefix("api/utils")]
    public class UtilitiesController : BaseApiController
    {
        private readonly UserExpenseContext _dbContext = new UserExpenseContext();

        [Route("categories/{filter}")]
        public IEnumerable<string> GetCategories(string filter)
        {
            var categories = from e in _dbContext.Expenses
                where e.Category.ToLower().Contains(filter.ToLower())
                group e by new {e.Category}
                into g
                orderby g.Key.Category
                select g.Key.Category;

            return categories.Take(5).ToList();
        }
    }
}

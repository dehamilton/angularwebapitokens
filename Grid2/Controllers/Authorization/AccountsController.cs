using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserExpense.WebApi.Infrastructure;
using Microsoft.AspNet.Identity;
using UserExpense.WebApi.Models.Authorization;

namespace UserExpense.WebApi.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        [Authorize]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        [Authorize]
        [Route("currentuser")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            return await GetUser(User.Identity.GetUserId());
        }

        [Authorize]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Authorize]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                RegisterDate = DateTime.Now.Date,
            };

            IdentityResult addUserResult = await AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }
    }
}

using HrAPI.Base;
using HrAPI.Model;
using HrAPI.Repositories.Data;
using HrAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsRepository accountsRepository;

        public AccountsController(AccountsRepository accountsRepository)
        {
            this.accountsRepository = accountsRepository;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginVm login)
        {
            var response = await accountsRepository.Login(login);

            if (response != null)
            {
                return StatusCode(200,
                    new
                    {
                        Status = HttpStatusCode.OK,
                        Message = "Login successfully",
                        Data = response
                    });
            }
            return StatusCode(404,
            new
            {
                Status = HttpStatusCode.BadRequest,
                Message = "NIK or Password is invalid",
                Data = response
            }); ;
        }
        [HttpGet("Admins")]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Email}, yuo are an");
        }
        private LoginVm GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new LoginVm
                {
                    
                    Email = userClaims.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value,
                
                };
            }
            return null;
        }
    }
}

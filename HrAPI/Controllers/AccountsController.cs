using HrAPI.Base;
using HrAPI.Model;
using HrAPI.Repositories.Data;
using HrAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        //[Route("Login")]
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
            else if(response == "400")
            {
                return StatusCode(400,
                    new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "NIK 0r Password is invalid",
                        Data = response
                    });
            }
            return StatusCode(404,
            new
            {
                Status = HttpStatusCode.BadRequest,
                Message = "Login Gagal"
            });
        }
    }
}

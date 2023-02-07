using HrAPI.Base;
using HrAPI.Model;
using HrAPI.Repositories.Data;
using HrAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Accounts, AccountsRepository, string>
    {
        private readonly AccountsRepository accountsRepository;

        public AccountsController(AccountsRepository accountsRepository) : base(accountsRepository)
        {
            this.accountsRepository = accountsRepository;
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginVm login)
        {
            var response = accountsRepository.Login(login);

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
            else
            {
                return StatusCode(400,
                    new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "NIK 0r Password is invalid",
                        Data = response
                    });
            }
        }
    }
}

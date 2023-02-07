using HrAPI.Base;
using HrAPI.Model;
using HrAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRoles, AccountRolesRepository, string>
    {
        private readonly AccountRolesRepository accountRolesRepository;
        public AccountRolesController(AccountRolesRepository accountRolesRepository) : base(accountRolesRepository)
        {
            this.accountRolesRepository = accountRolesRepository;
        }
    }
}

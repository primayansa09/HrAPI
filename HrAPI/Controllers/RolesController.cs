using HrAPI.Base;
using HrAPI.Model;
using HrAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Roles, RolesRepository, string>
    {
        private readonly RolesRepository rolesRepository;

        public RolesController(RolesRepository rolesRepository) : base(rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }
    }
}

using HrAPI.Base;
using HrAPI.Model;
using HrAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : BaseController<Departements, DepartementRepository, int>
    {

        public DepartementController(DepartementRepository departementRepository) : base(departementRepository)
        {
        }
    }
}

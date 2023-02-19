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
    public class EmployeesController : BaseController<Employees, EmployeesRepository, string>
    {
        private readonly EmployeesRepository employeesRepository;

        public EmployeesController(EmployeesRepository employeesRepository) : base(employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterVm registerVm)
        {
            var response = employeesRepository.Register(registerVm);
            if(response == 1)
            {
                return StatusCode(200,
                    new
                    {
                        Status = HttpStatusCode.Created,
                        Message = "Data successfully resgister",
                        Data = response
                    });
            }else if(response == 2)
            {
                return StatusCode(400,
                    new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Messsage = "Data email is duplicate",
                        Data = response
                    });
            }else if(response == 3)
            {
                return StatusCode(400,
                    new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Data phone is duplicate",
                        Data = response
                    });
            }
            else
            {
                return StatusCode(500,
                    new
                    {
                        Status = HttpStatusCode.InternalServerError,
                        Message = "Internal server error",
                        Data = response
                    });
            }
        }
    }
}

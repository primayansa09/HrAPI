using HrAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HrAPI.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public virtual ActionResult Create(Entity entity)
        {
            var response = repository.Create(entity);
            if(response == 1)
            {
                return StatusCode(200,
                    new
                    {
                        Status = HttpStatusCode.Created,
                        Message = "Data successfully created",
                        Data = response
                    });
            }
            else if(response == 0)
            {
                return StatusCode(400,
                    new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Data failed to create",
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
        [HttpGet]
        public virtual IActionResult Get()
        {
            var response = repository.Get();
            if (response.Count() >= 1)
            {
                return StatusCode(200, 
                    new 
                    { 
                        Status = HttpStatusCode.OK, 
                        Message = $"{response.Count()} data found", 
                        Data = response 
                    });
            }
            else if (response.Count() == 0)
            {
                return StatusCode(404, 
                    new 
                    { 
                        Status = HttpStatusCode.NotFound, 
                        Message = "Data not found", 
                        Data = response 
                    });
            }
            else
            {
                return StatusCode(500, new { Status = HttpStatusCode.InternalServerError, Message = "Internal server error", Data = response });
            }
        }

        [HttpGet]
        [Route("{key}")]
        public virtual ActionResult Get(Key key)
        {
            var response = repository.Get(key);
            if (response != null)
            {
                return StatusCode(200, new { Status = HttpStatusCode.OK, Message = $"Data with code {key} found", Data = response });
            }
            else if (response == null)
            {
                return StatusCode(404, new { Status = HttpStatusCode.NotFound, Message = $"Data with code {key} not found", Data = response });
            }
            else
            {
                return StatusCode(500, new { Status = HttpStatusCode.InternalServerError, Message = "Internal server error", Data = response });
            }
        }

        [HttpPut]
        public virtual ActionResult Update(Entity entity, Key key)
        {
            var response = repository.Update(entity, key);
            if (response == 1)
            {
                return StatusCode(200, new { Status = HttpStatusCode.OK, Message = "Data successfully updated", Data = response });
            }
            else if (response == 0)
            {
                return StatusCode(400, new { Status = HttpStatusCode.BadRequest, Message = "Data failed to update", Data = response });
            }
            else
            {
                return StatusCode(500, new { Status = HttpStatusCode.InternalServerError, Message = "Internal server error", Data = response });
            }
        }

        [HttpDelete]
        [Route("{key}")]
        public virtual ActionResult Delete(Key key)
        {
            var response = repository.Delete(key);
            if (response == 1)
            {
                return StatusCode(200, new { Status = HttpStatusCode.OK, Message = "Data successfully deleted", Data = response });
            }
            else if (response == 0)
            {
                return StatusCode(400, new { Status = HttpStatusCode.BadRequest, Message = "Data failed to delete", Data = response });
            }
            else
            {
                return StatusCode(500, new { Status = HttpStatusCode.InternalServerError, Message = "Internal server error", Data = response });
            }
        }
    }
}

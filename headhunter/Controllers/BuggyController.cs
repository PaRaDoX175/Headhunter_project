using headhunter.Entities;
using headhunter.Errors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace headhunter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("Bad Request");
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            return StatusCode(500, "Internal Server Error");
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFound() 
        {
            var resp = StatusCode(404, new ApiException(404));
            return resp;
        }
    }
}

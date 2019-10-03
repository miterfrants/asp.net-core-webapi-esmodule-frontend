using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    [Route("api/v1")]
    public class UsersController : ControllerBase
    {
        [Route("users")]
        [HttpGet]
        public ActionResult<dynamic> Testing()
        {
            return new { name = "Peter Huang" };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Linq;

using Sample.Data;

namespace Sample.Controllers
{
    [Route("api/v1")]
    public class UsersController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public UsersController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("users")]
        [HttpGet]
        public ActionResult<dynamic> Testing()
        {
            return _dbContext.Users.Where(x => 1 == 1).ToList();
        }
    }
}

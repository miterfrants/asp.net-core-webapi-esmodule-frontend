using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Sample.Data;

namespace Sample.Controllers
{
    [Authorize]
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
        public ActionResult<dynamic> GetList([FromQuery] int page, [FromQuery] int limit)
        {
            return _dbContext.Users.Where(x => x.DeletedAt == null).OrderByDescending(x => x.Id).Skip(limit * page - 1).Take(limit)
            .Select(x => new { id = x.Id, email = x.Email }).ToList();
        }
    }
}

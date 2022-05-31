using System.Collections.Generic;
using System.Threading.Tasks;
using FactoryApp.Dtos;
using FactoryApp.Services.Base;
using FactoryApp.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace FactoryApp.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUsersListService usersListService;

        public UserController(IUsersListService usersListService)
        {
            this.usersListService = usersListService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<UsersListDto>> GetUsersList()
        {
            ServiceResult<IEnumerable<UsersListDto>> result = await usersListService.GetUsersListAsync();

            if (result.Ok)
            {
                return Ok(result.Extras);
            }

            return Problem(result.Message);
        }
    }
}
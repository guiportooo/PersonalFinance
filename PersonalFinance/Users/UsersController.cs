using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PersonalFinance.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService) => _usersService = usersService;

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(Guid id) =>
            await _usersService.GetUser(id) switch
            {
                { IsSuccess: true } result => Ok(result.Value),
                { IsSuccess: false } result => BadRequest(result.Error),
                _ => StatusCode(500)
            };
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Users.Models;

namespace PersonalFinance.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService) => _usersService = usersService;

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id) =>
            await _usersService.GetUser(id) switch
            {
                { IsSuccess: true } result => Ok(result.Value),
                { IsSuccess: false } result => BadRequest(result.Error),
                _ => StatusCode(500)
            };

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request) =>
            await _usersService.CreateUser(request) switch
            {
                { IsSuccess: true } result => CreatedAtRoute("Get", new {id = result.Value.Id}, result.Value),
                { IsSuccess: false } result => BadRequest(result.Error),
                _ => StatusCode(500)
            };
    }
}
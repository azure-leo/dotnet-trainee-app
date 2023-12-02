using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeAPI.Data;
using TraineeAPI.Services.UserService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraineeAPI.Controllers
{
    [Route("api/[controller]")]

    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            if (result is null)
            {
                return NotFound("This user does not exist");
            }
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser([FromBody] User user)
        {
            var result = await _userService.AddUser(user);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, [FromBody] User request)
        {
            var result = await _userService.UpdateUser(id, request);
            if (result is null)
            {
                return NotFound("This user does not exist");
            }
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result is null)
            {
                return NotFound("This user does not exist");
            }
            return Ok(result);
        }
        
    }
}


using FinanaceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            ServiceResponse<IEnumerable<User>> response = await _userService.GetAllUsers();
            return Ok(response);
        }
        [HttpGet("{email}/{password}")]
        public async Task<IActionResult> Get(UserLoginDTO userLoginDTO)
        {
            ServiceResponse<User> response = await _userService.GetUserByEmailAndPassword(userLoginDTO);
            if (response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserSaveDTO userSaveDTO)
        {
            ServiceResponse<User> response = await _userService.CreateUser(userSaveDTO);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Created(Request.HttpContext.ToString() , response);
        }
        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            ServiceResponse<User> response = await _userService.UpdateUser(user);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            ServiceResponse<User> response = await _userService.DeleteUser(Id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
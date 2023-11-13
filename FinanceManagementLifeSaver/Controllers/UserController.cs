using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            ServiceResponse<IEnumerable<User>> response = await _userService.GetAllUsers();
            return Ok(response);
        }
        [HttpGet("{email}/{password}")]
        public async Task<IActionResult> Get(string email, string password)
        {
            ServiceResponse<User> response = await _userService.GetUserByEmailAndPassword(
                new UserLoginDTO{ 
                    Email = email, 
                    Password = password
                });
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<UserSaveDTO>>> Post(UserSaveDTO userSaveDTO)
        {
            ServiceResponse<UserSaveDTO> response = new ServiceResponse<UserSaveDTO>();
            response.Data = _mapper.Map<UserSaveDTO>(await _userService.CreateUser(userSaveDTO));
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Created(Request.HttpContext.ToString() , response);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<UserSaveDTO>>> Put(UserSaveDTO user)
        {
            ServiceResponse<UserSaveDTO> response = await _userService.UpdateUser(user);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<User> response = await _userService.DeleteUser(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
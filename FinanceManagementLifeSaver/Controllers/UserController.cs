using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.DTO.UserDTO;
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
        private readonly INotificationService _notificationService;

        public UserController(IUserService userService, IMapper mapper, INotificationService notificationService)
        {
            _userService = userService;
            _mapper = mapper;
            _notificationService = notificationService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            ServiceResponse<IEnumerable<User>> response = await _userService.GetAllUsers();
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> GetUserLogin(UserLoginDTO user)
        {
            ServiceResponse<User> response = await _userService.GetUserByEmailAndPassword(user);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult<ServiceResponse<UserSaveDTO>>> AddUser(UserSaveDTO userSaveDTO)
        {
            ServiceResponse<UserSaveDTO> response = await _userService.CreateUser(userSaveDTO);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Created(Request.HttpContext.ToString() , response);
        }
        [HttpPut("UpdateUser")]
        public async Task<ActionResult<ServiceResponse<UserSaveDTO>>> UpdateUser(UserSaveDTO user)
        {
            ServiceResponse<UserSaveDTO> response = await _userService.UpdateUser(user);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            ServiceResponse<User> response = await _userService.DeleteUser(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost("ReportFailedLogin")]
        public async Task<IActionResult> ReportFailedLogin(FailedLoginDTO failedLoginDTO)
        {
            ServiceResponse<User> response = await _userService.GetUserByEmail(failedLoginDTO.Email);
            if (!response.Success)
            {
                return NotFound();
            }
            await _notificationService.CreateNotification(
                "Someone tried to log into your Account from " + failedLoginDTO.City + "," + failedLoginDTO.Country,
                response.Data.Id
                );
            return Ok();
        }
    }
}
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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Account>>>> GetAccountsByUserId(int userId)
        {
            ServiceResponse<IEnumerable<Account>> response = await _accountService.GetAccountsByUserId(userId);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Account>>> GetAccountById(int id)
        {
            ServiceResponse<Account> response = await _accountService.GetAccountById(id);
            if (!response.Success)
            {
                return NotFound(); // 404 Not Found, wenn der Account nicht existiert
            }
            return Ok(response); // 200 OK mit dem gefundenen Account
        }
    }
}

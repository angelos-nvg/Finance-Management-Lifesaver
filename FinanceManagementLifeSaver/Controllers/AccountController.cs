using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _accountService.GetAccountById(id);
            if (user == null)
            {
                return NotFound(); // 404 Not Found, wenn der Account nicht existiert
            }
            return Ok(user); // 200 OK mit dem gefundenen Account
        }
        [HttpPost]
        public async IActionResult Add(AccountSaveDto account)
        {
            await _accountService.AddAccount(account);
            return Ok(null);
        }
    }
}

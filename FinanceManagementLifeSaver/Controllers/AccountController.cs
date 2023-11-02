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
        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            var accounts = _accountService.GetAccountsByUserId();
            return Ok(accounts);
        }
        [HttpGet("{Accounts}")]
        public IActionResult Get(int id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound(); // 404 Not Found, wenn der Benutzer nicht existiert
            }
            return Ok(account); // 200 OK mit dem gefundenen Benutzer
        }
    }
}

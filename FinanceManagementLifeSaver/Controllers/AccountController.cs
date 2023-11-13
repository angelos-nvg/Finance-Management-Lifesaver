using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.DTO.AccountDTO;
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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccountsByUserId(int userId)
        {
            ServiceResponse<IEnumerable<Account>> response = await _accountService.GetAccountsByUserId(userId);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<AccountDTO>>> GetAccountById(int id)
        {
            ServiceResponse<AccountDTO> response = await _accountService.GetAccountById(id);
            if (!response.Success)
            {
                return NotFound(); // 404 Not Found, wenn der Account nicht existiert
            }
            return Ok(response); // 200 OK mit dem gefundenen Account
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AccountSaveDTO>>> Post(AccountSaveDTO accountCreateDTO)
        {
            ServiceResponse<AccountSaveDTO> response = await _accountService.CreateAccount(accountCreateDTO);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Created(Request.HttpContext.ToString(), response);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<AccountSaveDTO>>> Put(AccountSaveDTO accountDTO)
        {
            ServiceResponse<AccountSaveDTO> response = await _accountService.UpdateAccount(accountDTO);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<Account>>> Delete(int id) {
            ServiceResponse<Account> response = await _accountService.DeleteAccount(id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}

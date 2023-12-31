﻿using FinanceManagementLifesaver.DTO;
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
        [HttpPost("GetUserAccounts")]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccountsByUserId(UserIdDTO user)
        {
            ServiceResponse<IEnumerable<AccountDTO>> response = await _accountService.GetAccountsByUserId(user);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost("GetAccount")]
        public async Task<ActionResult<ServiceResponse<AccountDTO>>> GetAccountById(AccountIdDTO account)
        {
            ServiceResponse<AccountDTO> response = await _accountService.GetAccountById(account);
            if (!response.Success)
            {
                return NotFound(); // 404 Not Found, wenn der Account nicht existiert
            }
            return Ok(response); // 200 OK mit dem gefundenen Account
        }
        [HttpPost("AddAccount")]
        public async Task<ActionResult<ServiceResponse<AccountSaveDTO>>> AddAccount(AccountSaveDTO accountDTO)
        {
            ServiceResponse<AccountSaveDTO> response = await _accountService.CreateAccount(accountDTO);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Created(Request.HttpContext.ToString(), response);
        }
        [HttpPut("UpdateAccount")]
        public async Task<ActionResult<ServiceResponse<AccountSaveDTO>>> UpdateAccount(AccountSaveDTO accountDTO)
        {
            ServiceResponse<AccountSaveDTO> response = await _accountService.UpdateAccount(accountDTO);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("DeleteAccount")]
        public async Task<ActionResult<ServiceResponse<Account>>> DeleteAccount(AccountIdDTO account) {
            ServiceResponse<Account> response = await _accountService.DeleteAccount(account);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}

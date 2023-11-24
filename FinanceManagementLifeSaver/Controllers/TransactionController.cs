using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.DTO.AccountDTO;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using FinanceManagementLifesaver.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpPost("TransactionsByAccount/")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByAccountId(AccountIdDTO account)
        {
            ServiceResponse<IEnumerable<Transaction>>response = await _transactionService.GetTransactionsByAccountId(account);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost("id")]
        public async Task<ActionResult<ServiceResponse<TransactionDTO>>> GetTransactionById(TransactionIdDTO id)
        {
            ServiceResponse<TransactionDTO> response = await _transactionService.GetTransactionById(id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<TransactionSaveDTO>>> Post(TransactionSaveDTO transaction)
        {
            ServiceResponse<TransactionSaveDTO> response = await _transactionService.CreateTransaction(transaction);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<TransactionSaveDTO>>> Put(TransactionSaveDTO transaction)
        {
            ServiceResponse<TransactionSaveDTO> response = await _transactionService.UpdateTransaction(transaction);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(TransactionIdDTO id)
        {
            ServiceResponse<TransactionDTO> response = await _transactionService.DeleteTransaction(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}

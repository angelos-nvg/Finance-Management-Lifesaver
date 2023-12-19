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
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpPost("TransactionsByAccount")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByAccountId(AccountIdDTO account)
        {
            ServiceResponse<IEnumerable<TransactionDTO>>response = await _transactionService.GetTransactionsByAccountId(account);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost("GetTransaction")]
        public async Task<ActionResult<ServiceResponse<TransactionDTO>>> GetTransactionById(TransactionIdDTO id)
        {
            ServiceResponse<TransactionDTO> response = await _transactionService.GetTransactionById(id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("AddTransaction")]
        public async Task<ActionResult<ServiceResponse<TransactionSaveDTO>>> AddTransaction(TransactionSaveDTO transaction)
        {
            ServiceResponse<TransactionSaveDTO> response = await _transactionService.CreateTransaction(transaction);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateTransaction")]
        public async Task<ActionResult<ServiceResponse<TransactionSaveDTO>>> UpdateTransaction(TransactionSaveDTO transaction)
        {
            ServiceResponse<TransactionSaveDTO> response = await _transactionService.UpdateTransaction(transaction);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteTransaction")]
        public async Task<ActionResult> DeleteTransaction(TransactionIdDTO id)
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

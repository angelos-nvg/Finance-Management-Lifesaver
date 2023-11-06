using FinanaceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using FinanceManagementLifesaver.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet("TransactionByAccount/{accountId}")]
        public ActionResult<IEnumerable<Transaction>> GetTransactionsByAccountId(AccountIdDTO accountId)
        {
            var transactions = _transactionService.GetTransactionsByAccountId(accountId);
            return Ok(transactions);
        }
        [HttpGet("{id}")]
        public IActionResult GetTransactionById(TransactionIdDTO transactionId)
        {
            var transaction = _transactionService.GetTransactionById(transactionId);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TransactionSaveDTO transaction)
        {
            ServiceResponse<TransactionSaveDTO> response = await _transactionService.CreateTransaction(transaction);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Created(Request.HttpContext.ToString(), response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(TransactionDTO transaction)
        {
            ServiceResponse<TransactionDTO> response = await _transactionService.UpdateTransaction(transaction);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(TransactionIdDTO transaction)
        {
            ServiceResponse<Transaction> response = await _transactionService.DeleteTransaction(transaction);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}

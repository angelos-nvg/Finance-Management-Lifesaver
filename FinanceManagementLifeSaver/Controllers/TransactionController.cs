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
        [HttpGet("TransactionsByAccount/{accountId}")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByAccountId(int accountId)
        {
            ServiceResponse<IEnumerable<Transaction>>response = await _transactionService.GetTransactionsByAccountId(accountId);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<TransactionDTO>>> GetTransactionById(int id)
        {
            ServiceResponse<TransactionDTO> response = await _transactionService.GetTransactionById(id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<TransactionDTO>>> Post(TransactionSaveDTO transaction)
        {
            ServiceResponse<TransactionSaveDTO> response = await _transactionService.CreateTransaction(transaction);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<Transaction>>> Put(TransactionDTO transaction)
        {
            ServiceResponse<Transaction> response = await _transactionService.UpdateTransaction(transaction);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(TransactionIdDTO transaction)
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

using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentController(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }
        [HttpGet("InvestmentsByAccount/{accountId}")]
        public async Task<ActionResult<IEnumerable<InvestmentDTO>>> GetInvestmentsByAccountId(int accountId)
        {
            ServiceResponse<IEnumerable<Investment>> response = await _investmentService.GetInvestmentsByAccountId(accountId);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<InvestmentDTO>>> GetInvestmentById(int id)
        {
            ServiceResponse<InvestmentDTO> response = await _investmentService.GetInvestmentById(id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<InvestmentDTO>>> Post(InvestmentDTO investment)
        {
            ServiceResponse<InvestmentDTO> response = await _investmentService.CreateInvestment(investment);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<InvestmentDTO>>> Put(InvestmentDTO investment)
        {
            ServiceResponse<InvestmentDTO> response = await _investmentService.UpdateInvestment(investment);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ServiceResponse<Investment> response = await _investmentService.DeleteInvestment(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}

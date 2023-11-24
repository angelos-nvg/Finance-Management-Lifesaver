using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.DTO.AccountDTO;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Migrations;
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
        [HttpPost("InvestmentsByAccount")]
        public async Task<ActionResult<IEnumerable<InvestmentDTO>>> GetInvestmentsByAccountId(AccountIdDTO accountId)
        {
            ServiceResponse<IEnumerable<Investment>> response = await _investmentService.GetInvestmentsByAccountId(accountId);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost("GetInvestment")]
        public async Task<ActionResult<ServiceResponse<InvestmentDTO>>> GetInvestmentById(InvestmentIdDTO investment)
        {
            ServiceResponse<InvestmentDTO> response = await _investmentService.GetInvestmentById(investment.Id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("InvestmentsByTime")]
        public async Task<ActionResult<IEnumerable<InvestmentDTO>>> GetInvestmentsTillMonthBack(InvestmentTimeDTO InvestTimeDTO)
        {
            ServiceResponse<IEnumerable<InvestmentDTO>> response = await _investmentService.GetInvestmentsTillMonthBack(InvestTimeDTO);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("AddInvestment")]
        public async Task<ActionResult<ServiceResponse<InvestmentDTO>>> AddInvestment(InvestmentDTO investment)
        {
            ServiceResponse<InvestmentDTO> response = await _investmentService.CreateInvestment(investment);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("InvestmentsByRoI")]
        public async Task<ActionResult<IEnumerable<ServiceResponse<InvestmentDTO>>>> GetInvestmentsByRoI(InvestmentIdDTO scope)
        {
            ServiceResponse<IEnumerable<InvestmentDTO>> response = await _investmentService.GetInvestmentsByRoI(scope);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut("UpdateInvestment")]
        public async Task<ActionResult<ServiceResponse<InvestmentDTO>>> UpdateInvestment(InvestmentDTO investment)
        {
            ServiceResponse<InvestmentDTO> response = await _investmentService.UpdateInvestment(investment);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteInvestment")]
        public async Task<ActionResult> DeleteInvestment(int id)
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

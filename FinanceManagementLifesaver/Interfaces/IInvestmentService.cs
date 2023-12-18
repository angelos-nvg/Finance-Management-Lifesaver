using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IInvestmentService
    {
        Task<ServiceResponse<InvestmentDTO>> CreateInvestment(InvestmentDTO investment);
        Task<ServiceResponse<InvestmentDTO>> GetInvestmentById(InvestmentIdDTO investmentId);
        Task<ServiceResponse<IEnumerable<Investment>>> GetInvestmentsByAccountId(AccountIdDTO accountId);
        Task<ServiceResponse<InvestmentDTO>> UpdateInvestment(InvestmentDTO investment);
        Task<ServiceResponse<Investment>> DeleteInvestment(int investmentId);
        Task<ServiceResponse<IEnumerable<InvestmentDTO>>> GetInvestmentsByRoI(InvestmentIdDTO scopeId);
        Task<ServiceResponse<IEnumerable<InvestmentDTO>>> GetInvestmentsTillMonthBack(InvestmentTimeDTO iTD);
    }
}

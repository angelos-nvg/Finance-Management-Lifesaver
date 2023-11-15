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
        Task<ServiceResponse<InvestmentDTO>> GetInvestmentById(int investmentId);
        Task<ServiceResponse<IEnumerable<Investment>>> GetInvestmentsByAccountId(int accountId);
        Task<ServiceResponse<InvestmentDTO>> UpdateInvestment(InvestmentDTO investment);
        Task<ServiceResponse<Investment>> DeleteInvestment(int investmentId);
    }
}

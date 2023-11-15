using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.ServiceResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IInvestmentService
    {
        Task<ServiceResponse<InvestmentSaveDTO>> CreateInvestment(InvestmentSaveDTO investment);
        Task<ServiceResponse<InvestmentDTO>> GetInvestmentById(int investmentId);
        Task<ServiceResponse<IEnumerable<InvestmentDTO>>> GetInvestmentsByAccountId(int accountId);
        Task<ServiceResponse<InvestmentSaveDTO>> UpdateInvestment(InvestmentSaveDTO investment);
        Task<ServiceResponse<InvestmentDTO>> DeleteInvestment(InvestmentIdDTO investmentId);
    }
}

using AutoMapper;
using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.ServiceResponse;
using FinanceManagementLifesaver.Validations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Services
{
    public class InvestmentService
    {
        public class InvestmentService : IInvestmentService
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public InvestmentService(IMapper mapper, DataContext context)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<ServiceResponse<InvestmentSaveDTO>> CreateInvestment(InvestmentSaveDTO account)
            {
                ServiceResponse<InvestmentSaveDTO> response = new ServiceResponse<InvestmentSaveDTO>();
                var _user = await _context.Users.FirstOrDefaultAsync(u => u.Id == account.UserId);
                if (_user == null)
                {
                    response.Message = "Benutzer wurde nicht gefunden";
                    return response;
                }
                account.UserId = _user.Id;

                //Validation
                InvestmentValidation validator = new InvestmentValidation();
                var result = validator.Validate(account);
                response.Message = ValidationResponse.GetValidatorResponse(result.IsValid, result.Errors);
                if (response.Message == "")
                {
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    return response;
                }

                Investment insertInvestment = new Investment();
                insertInvestment.InvestmentBalance = account.InvestmentBalance;
                insertInvestment.InvestmentType = account.InvestmentType;
                insertInvestment.User = _user;
                insertInvestment.Name = account.Name;
                insertInvestment.Id = account.Id;

                await _context.Investments.AddAsync(insertInvestment);
                await _context.SaveChangesAsync();
                response.Data = account;
                return response;
            }

            public async Task<ServiceResponse<InvestmentDTO>> GetInvestmentById(int accountId)
            {
                ServiceResponse<InvestmentDTO> response = new ServiceResponse<InvestmentDTO>();
                Investment account = await _context.Investments.FirstOrDefaultAsync(u => u.Id == accountId);
                if (account == null)
                {
                    response.Success = false;
                    return response;
                }
                response.Data = _mapper.Map<Investment, InvestmentDTO>(account);
                return response;
            }

            public async Task<ServiceResponse<IEnumerable<Investment>>> GetInvestmentsByUserId(int userId)
            {
                ServiceResponse<IEnumerable<Investment>> response = new ServiceResponse<IEnumerable<Investment>>();
                List<Investment> accounts = (List<Investment>)await _context.Investments.Where(u => u.User.Id == userId).ToListAsync();
                if (!accounts.Any())
                {
                    response.Success = false;
                    return response;
                }
                response.Data = accounts;
                return response;
            }

            public async Task<ServiceResponse<InvestmentSaveDTO>> UpdateInvestment(InvestmentSaveDTO account)
            {
                ServiceResponse<InvestmentSaveDTO> response = new ServiceResponse<InvestmentSaveDTO>();
                Investment _account = await _context.Investments.FirstOrDefaultAsync(u => u.Id == account.Id);
                if (_account == null)
                {
                    response.Success = false;
                    response.Message = "Investment not found";
                    return response;
                }
                var _user = await _context.Users.FirstOrDefaultAsync(u => u.Id == account.UserId);
                if (_user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }
                _account.InvestmentBalance = account.InvestmentBalance;
                _account.InvestmentType = account.InvestmentType;
                _account.Name = account.Name;
                _account.User = new User { Id = account.UserId };

                //Validation
                InvestmentValidation validator = new InvestmentValidation();
                var result = validator.Validate(account);
                response.Message = ValidationResponse.GetValidatorResponse(result.IsValid, result.Errors);
                if (response.Message == "")
                {
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    return response;
                }

                _context.Investments.Update(_account);
                await _context.SaveChangesAsync();
                response.Data = account;
                return response;
            }

            public async Task<ServiceResponse<Investment>> DeleteInvestment(int accountId)
            {
                ServiceResponse<Investment> response = new ServiceResponse<Investment>();
                Investment account = await _context.Investments.FirstOrDefaultAsync(u => u.Id == accountId);
                if (account == null)
                {
                    response.Success = false;
                    return response;
                }
                List<Transaction> transactions = await _context.Transactions.Where(t => t.Investment.Id == account.Id).ToListAsync();
                if (transactions.Count > 0)
                {
                    _context.Transactions.RemoveRange(transactions);
                }
                _context.Investments.Remove(account);
                await _context.SaveChangesAsync();
                response.Data = account;
                return response;
            }
        }
    }
}

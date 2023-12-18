using AutoMapper;
using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Migrations;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using FinanceManagementLifesaver.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Services
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

            public async Task<ServiceResponse<InvestmentDTO>> CreateInvestment(InvestmentDTO investment)
            {
                ServiceResponse<InvestmentDTO> response = new ServiceResponse<InvestmentDTO>();
                var _account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == investment.Account.Id);
                if (_account == null)
                {
                    response.Message = "Benutzer wurde nicht gefunden";
                    return response;
                }

                //Validation
                InvestmentValidations validator = new InvestmentValidations();
                var result = validator.Validate(investment);
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

                Investment insertInvestment = new Investment
                {
                    InvestedMoney = investment.InvestedMoney,
                    InvestmentType = investment.InvestmentType,
                    Account = _account,
                    GrossIncome = investment.GrossIncome,
                    StartMoney = investment.StartMoney,
                    StartDate = investment.StartDate,
                    Id = investment.Id,        
                };

                await _context.Investments.AddAsync(insertInvestment);
                await _context.SaveChangesAsync();
                response.Data = investment;
                return response;
            }


        public async Task<ServiceResponse<InvestmentDTO>> GetInvestmentById(InvestmentIdDTO investmentId)
        {
            ServiceResponse<InvestmentDTO> response = new ServiceResponse<InvestmentDTO>();
            Investment investment = await _context.Investments.FirstOrDefaultAsync(u => u.Id == investmentId.Value);
            if (investment == null)
            {
                response.Success = false;
                return response;
            }
            response.Data = _mapper.Map<Investment, InvestmentDTO>(investment);
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<Investment>>> GetInvestmentsByAccountId(AccountIdDTO account)
            {
                ServiceResponse<IEnumerable<Investment>> response = new ServiceResponse<IEnumerable<Investment>>();
                List<Investment> investments = await _context.Investments.Where(i => i.Account.Id == account.Value).ToListAsync();
                if (!investments.Any())
                {
                    response.Success = false;
                    return response;
                }
                response.Data = investments;
                return response;
            }

            public async Task<ServiceResponse<InvestmentDTO>> UpdateInvestment(InvestmentDTO investment)
            {
                ServiceResponse<InvestmentDTO> response = new ServiceResponse<InvestmentDTO>();
                Investment _investment = await _context.Investments.FirstOrDefaultAsync(i => i.Id == investment.Id);
                if (_investment == null)
                {
                    response.Success = false;
                    response.Message = "Investment not found";
                    return response;
                }else
                {
                    _investment.StartDate =investment.StartDate;
                    _investment.StartMoney = investment.StartMoney;
                    _investment.Account = investment.Account;
                    _investment.InvestedMoney = investment.InvestedMoney;
                    _investment.GrossIncome = investment.GrossIncome;
                    _investment.InvestmentType = investment.InvestmentType;
                }
                var _account = await _context.Users.FirstOrDefaultAsync(a => a.Id == investment.Account.Id);
                if (_account == null)
                {
                    response.Success = false;
                    response.Message = "Account not found";
                    return response;
                }

                //Validation
                InvestmentValidations validator = new InvestmentValidations();
                var result = validator.Validate(investment);
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
                _context.Investments.Update(_investment);
                await _context.SaveChangesAsync();
                response.Data = investment;
                return response;
            }

            public async Task<ServiceResponse<Investment>> DeleteInvestment(int investmendId)
            {
                ServiceResponse<Investment> response = new ServiceResponse<Investment>();
                Investment investment = await _context.Investments.FirstOrDefaultAsync(i => i.Id == investmendId);
                if (investment == null)
                {
                    response.Success = false;
                    return response;
                }
                _context.Investments.Remove(investment);
                await _context.SaveChangesAsync();
                response.Data = investment;
                return response;
            }

        public async Task<ServiceResponse<IEnumerable<InvestmentDTO>>> GetInvestmentsByRoI(InvestmentIdDTO scope)
        {
            ServiceResponse<IEnumerable<InvestmentDTO>> response = new ServiceResponse<IEnumerable<InvestmentDTO>>();
            List<InvestmentDTO> investments = _mapper.Map<List<InvestmentDTO>>(await _context.Investments.Where(i => i.Account.Id == scope.Value).ToListAsync());
            response.Data = investments.OrderByDescending(i => i.RoI);
            return response;
        }
        public async Task<ServiceResponse<IEnumerable<InvestmentDTO>>> GetInvestmentsTillMonthBack(InvestmentTimeDTO ITD)
        {
            if(!InvestmentValidations.IsTimeFrameValid(ITD.Timeframe))
            {
                ITD.Timeframe = 3;
            }
            var filter = DateTime.Today.AddMonths(ITD.Timeframe);
            ServiceResponse<IEnumerable<InvestmentDTO>> response = new ServiceResponse<IEnumerable<InvestmentDTO>>();
            List<InvestmentDTO> investments = _mapper.Map<List<InvestmentDTO>>(await _context.Investments.Where(i => i.StartDate < filter && i.Account.Id == ITD.ScopeId).ToListAsync());
            investments.OrderBy(i => i.RoI);
            response.Data = investments;
            return response;
        }
    }
}

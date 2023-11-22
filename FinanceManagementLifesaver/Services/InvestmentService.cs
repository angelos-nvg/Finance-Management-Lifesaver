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
                    RoI = investment.RoI,
                    StartMoney = investment.StartMoney,
                    StartDate = investment.StartDate,
                    Id = investment.Id
                };

                await _context.Investments.AddAsync(insertInvestment);
                await _context.SaveChangesAsync();
                response.Data = investment;
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

            public async Task<ServiceResponse<IEnumerable<Investment>>> GetInvestmentsByAccountId(int accountId)
            {
                ServiceResponse<IEnumerable<Investment>> response = new ServiceResponse<IEnumerable<Investment>>();
                List<Investment> investments = (List<Investment>)await _context.Investments.Where(a => a.Id == accountId).ToListAsync();
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
                    _investment.RoI = investment.RoI;
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

        public async Task<ServiceResponse<IEnumerable<InvestmentDTO>>> GetInvestmentsByRoI(int scopeId)
        {
            ServiceResponse<IEnumerable<InvestmentDTO>> response = new ServiceResponse<IEnumerable<InvestmentDTO>>();
            List<Investment> investments = await _context.Investments.Where(i => i.Account.Id == scopeId).ToListAsync();
            investments.OrderBy(i => i.RoI);
            response.Data = (IEnumerable<InvestmentDTO>)investments;
            return response;
        }
        public async Task<ServiceResponse<IEnumerable<InvestmentDTO>>> GetInvestmentsTillMonthBack(int timeframe)
        {
            if(!InvestmentValidations.IsTimeFrameValid(timeframe))
            {
                timeframe = 3;
            }
            var filter = DateTime.Today.AddMonths(-timeframe);
            ServiceResponse<IEnumerable<InvestmentDTO>> response = new ServiceResponse<IEnumerable<InvestmentDTO>>();
            IEnumerable<Investment> investments = await _context.Investments.Where(i => i.StartDate < filter).ToListAsync();
            investments.OrderBy(i => i.RoI);
            response.Data = (IEnumerable<InvestmentDTO>)investments;
            return response;
        }
    }
}

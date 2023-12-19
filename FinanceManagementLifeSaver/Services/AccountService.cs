using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagementLifesaver.DTO.AccountDTO;
using FinanceManagementLifesaver.Validations;
using Microsoft.AspNetCore.Routing.Matching;

namespace FinanceManagementLifesaver.Services
{
	public class AccountService : IAccountService
	{
		private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        public AccountService(IMapper mapper, INotificationService notificationService, DataContext context)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _context = context;
        }

        public async Task<ServiceResponse<AccountSaveDTO>> CreateAccount(AccountSaveDTO account)
		{
			ServiceResponse<AccountSaveDTO> response = new ServiceResponse<AccountSaveDTO>();
            var _user = await _context.Users.FirstOrDefaultAsync(u => u.Id == account.UserId);
            if (_user == null)
            {
                response.Message = "Benutzer wurde nicht gefunden";
                return response;
            }
            account.UserId = _user.Id;

            //Validation
            AccountValidation validator = new AccountValidation();
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

            Account insertAccount = new Account
            {
                AccountBalance = account.AccountBalance,
                AccountType = account.AccountType,
                User = _user,
                Name = account.Name,
                Id = account.Id
            };

            await _context.Accounts.AddAsync(insertAccount);
			await _context.SaveChangesAsync();
            response.Data = account;
            return response;
		}

        public async Task<ServiceResponse<AccountDTO>> GetAccountById(AccountIdDTO accountId)
        {
			ServiceResponse<AccountDTO> response = new ServiceResponse<AccountDTO>();
            Account account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == accountId.Value);
            if (account == null)
            {
                response.Success = false;
                return response;
            }
            response.Data = _mapper.Map<Account,AccountDTO>(account);
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountsByUserId(UserIdDTO user)
        {
            ServiceResponse<IEnumerable<AccountDTO>> response = new ServiceResponse<IEnumerable<AccountDTO>>();
            List<AccountDTO> accounts = _mapper.Map<List<AccountDTO>>(await _context.Accounts.Where(u => u.User.Id == user.Value).ToListAsync());
            if (!accounts.Any())
            {
                response.Success = false;
                return response;
            }
            response.Data = accounts;
            return response;
        }
        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAllAccounts()
        {
            ServiceResponse<IEnumerable<AccountDTO>> response = new ServiceResponse<IEnumerable<AccountDTO>>();
            List<AccountDTO> accounts = _mapper.Map<List<AccountDTO>>(await _context.Accounts.ToListAsync());
            if (!accounts.Any())
            {
                response.Success = false;
                return response;
            }
            response.Data = accounts;
            return response;
        }

        public async Task<ServiceResponse<AccountSaveDTO>> UpdateAccount(AccountSaveDTO account)
        {
			ServiceResponse<AccountSaveDTO> response = new ServiceResponse<AccountSaveDTO>();
            Account _account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == account.Id);
            if (_account == null)
            {
                response.Success = false;
                response.Message = "Konto nicht gefunden";
                return response;
            }
            var _user = await _context.Users.FirstOrDefaultAsync(u => u.Id == account.UserId);
            if (_user == null)
            {
                response.Success = false;
                response.Message = "Benutzer nicht gefunden";
                return response;
            }
            _account.AccountBalance = account.AccountBalance;
            _account.AccountType = account.AccountType;
            _account.Name = account.Name;
            _account.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == account.UserId);
            _account.ScopeId = account.ScopeId;

            //Validation
            AccountValidation validator = new AccountValidation();
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

            _context.Accounts.Update(_account);
            await _context.SaveChangesAsync();
            await _notificationService.CheckIfAccountBalanceNegative(_account);
            response.Data = account;
			return response;
        }

        public async Task<ServiceResponse<Account>> DeleteAccount(AccountIdDTO accountId)
		{
			ServiceResponse<Account> response = new ServiceResponse<Account>();
			Account account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == accountId.Value);
            if (account == null)
            {
                response.Success = false;
                return response;
            }
            List<Transaction> transactions = await _context.Transactions.Where(t => t.Account.Id == account.Id).ToListAsync();
            if (transactions.Count > 0 )
            {
                 _context.Transactions.RemoveRange(transactions);
            }
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            response.Data = account;
			return response;
		}
	}
}
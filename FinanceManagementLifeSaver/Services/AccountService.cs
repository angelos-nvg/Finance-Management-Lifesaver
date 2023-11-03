using AutoMapper;
using FinanaceManagementLifesaver.DTO;
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

namespace FinanceManagementLifesaver.Services
{
	public class AccountService : IAccountService
	{
		private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AccountService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<Account>> CreateAccount(AccountSaveDTO account)
		{
			ServiceResponse<Account> response = new ServiceResponse<Account>();
            Account _account = _mapper.Map<AccountSaveDTO, Account>(account);
            await _context.Accounts.AddAsync(_account);
			await _context.SaveChangesAsync();
            response.Data = _account;
            return response;
		}

        public async Task<ServiceResponse<AccountDTO>> GetAccountById(int accountId)
        {
			ServiceResponse<AccountDTO> response = new ServiceResponse<AccountDTO>();
            Account account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == accountId);
            response.Data = _mapper.Map<AccountDTO>(_context.Accounts.FirstOrDefaultAsync(u => u.Id == accountId));
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountsByUserId(int userId)
        {
            ServiceResponse<IEnumerable<AccountDTO>> response = new ServiceResponse<IEnumerable<AccountDTO>>();
            IEnumerable<Account> accounts = (IEnumerable<Account>)await _context.Accounts.Select(u => u.Id == userId).ToListAsync();
            if (!accounts.Any())
            {
                response.Success = false;
                return response;
            }
            response.Data = _mapper.Map<IEnumerable<Account>, IEnumerable<AccountDTO>>(accounts);
            return response;
        }

        public async Task<ServiceResponse<Account>> UpdateAccount(Account account)
        {
			ServiceResponse<Account> response = new ServiceResponse<Account>();
            Account _account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == account.Id);
            _account.AccountBalance = account.AccountBalance;
            _account.AccountType = account.AccountType;
            _account.User = account.User;
            _account.Transactions = account.Transactions;
            _context.Accounts.Update(_account);
            await _context.SaveChangesAsync();
            response.Data = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == account.Id);
			return response;
        }

        public async Task<ServiceResponse<Account>> DeleteAccount(int accountId)
		{
			ServiceResponse<Account> response = new ServiceResponse<Account>();
			Account account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == accountId);
            if (account == null)
            {
                response.Success = false;
                return response;
            }
			_context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            response.Data = account;
			return response;
		}
	}
}
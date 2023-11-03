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

        public AccountService(DataContext context)
		{
			_context = context;
		}

        public AccountService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Account>> CreateAccount(Account account)
		{
			ServiceResponse<Account> response = new ServiceResponse<Account>();
			await _context.Accounts.AddAsync(account);
			await _context.SaveChangesAsync();
			response.Data = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == account.Id);
			return response;
		}

        public async Task<ServiceResponse<Account>> GetAccountById(int accountId)
        {
			ServiceResponse<Account> response = new ServiceResponse<Account>();
            Account account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == accountId);
			response.Data = account;
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<Account>>> GetAccountsByUserId(int userId)
        {
            ServiceResponse<IEnumerable<Account>> response = new ServiceResponse<IEnumerable<Account>>();
            IEnumerable<Account> accounts = (IEnumerable<Account>)await _context.Accounts.Select(u => u.Id == userId).ToListAsync();
            response.Data = accounts;
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
			_context.Accounts.Remove(account);
			response.Data = account;
			await _context.SaveChangesAsync();
			return response;
		}
	}
}
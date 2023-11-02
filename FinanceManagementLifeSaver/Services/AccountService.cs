using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
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

		public AccountService(DataContext context)
		{
			_context = context;
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

        public async Task<IEnumerable<ServiceResponse<Account>>> GetAccountsByUserId(int userId)
        {
            ServiceResponse<Account> response = new ServiceResponse<Account>();
            IEnumerable<Account> accounts = await _context.Accounts(u => u.Id == userId).ToListAsync();
            response.Data = accounts;
            return response;
        }

        public async Task<IEnumerable<ServiceResponse<Account>>> GetAccountById(int id)
        {
            ServiceResponse<Account> response = new ServiceResponse<Account>();
            Account account = await _context.Accounts(u => u.Id == id);
            response.Data = account;
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
            response.Data = await _context.Account.FirstOrDefaultAsync(a => a.Id == account.Id);
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
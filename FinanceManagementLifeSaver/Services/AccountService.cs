using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
		public async void CreateAccount(Account account)
		{
			await _context.Accounts.AddAsync(account);
			await _context.SaveChangesAsync();
		}

		public async void DeleteAccount(int accountId)
		{
			Account account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == accountId);
			_context.Accounts.Remove(account);
			await _context.SaveChangesAsync();
		}

		public async Task<Account> GetAccountById(int accountId)
		{
			Account account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == accountId);
			return account;
		}

		public async void UpdateAccount(Account account)
		{
			Account _account = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == account.Id);
			_account.AccountBalance = account.AccountBalance;
			_account.AccountType = account.AccountType;
			_account.User = account.User;
			_account.Transactions = account.Transactions;
			_context.Accounts.Update(_account);
			await _context.SaveChangesAsync();
		}
	}
}
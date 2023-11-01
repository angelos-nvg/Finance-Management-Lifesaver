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
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }
        public async void CreateTransaction(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async void DeleteTransaction(int transactionId)
        {
            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transactionId);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transactionId);
            return transaction;
        }

        public async void UpdateTransaction(Transaction transaction)
        {
            Transaction _transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transaction.Id);
            _transaction.Amount = transaction.Amount;
            _transaction.TransactionType = transaction.TransactionType;
            _transaction.Date = transaction.Date;
            _transaction.Account = transaction.Account;
            _transaction.ReceiverAccount = transaction.ReceiverAccount;
            await _context.SaveChangesAsync();
        }
    }
}
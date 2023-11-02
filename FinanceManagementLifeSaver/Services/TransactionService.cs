using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
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
        public async Task<ServiceResponse<Transaction>> CreateTransaction(Transaction transaction)
        {
            ServiceResponse<Transaction> response = new ServiceResponse<Transaction>();
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            response.Data = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);
            return response;
        }

        public async Task<ServiceResponse<Transaction>> GetTransactionById(int transactionId)
        {
            ServiceResponse<Transaction> response = new ServiceResponse<Transaction>();
            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transactionId);
            response.Data = transaction;
            return response;
        }

        public async Task<ServiceResponse<Transaction>> UpdateTransaction(Transaction transaction)
        {
            ServiceResponse<Transaction> response = new ServiceResponse<Transaction>();
            Transaction _transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transaction.Id);
            _transaction.Amount = transaction.Amount;
            _transaction.TransactionType = transaction.TransactionType;
            _transaction.Date = transaction.Date;
            _transaction.Account = transaction.Account;
            _transaction.ReceiverAccount = transaction.ReceiverAccount;
            await _context.SaveChangesAsync();
            response.Data = transaction;
            return response;
        }

        public async Task<ServiceResponse<Transaction>> DeleteTransaction(int transactionId)
        {
            ServiceResponse<Transaction> response = new ServiceResponse<Transaction>();
            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transactionId);
            response.Data = transaction;
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return response;
        }
    }
}
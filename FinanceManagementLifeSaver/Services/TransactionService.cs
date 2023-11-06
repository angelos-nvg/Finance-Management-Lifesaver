using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TransactionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TransactionSaveDTO>> CreateTransaction(TransactionSaveDTO transaction)
        {
            ServiceResponse<TransactionSaveDTO> response = new ServiceResponse<TransactionSaveDTO>();
            await _context.Transactions.AddAsync(_mapper.Map< TransactionSaveDTO, Transaction>(transaction));
            await _context.SaveChangesAsync();
            response.Data = transaction;
            return response;
        }

        public async Task<ServiceResponse<TransactionDTO>> GetTransactionById(TransactionIdDTO transactionId)
        {
            ServiceResponse<TransactionDTO> response = new ServiceResponse<TransactionDTO>();
            response.Data = _mapper.Map<TransactionDTO>(_context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId.Id));
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<TransactionDTO>>> GetTransactionsByAccountId(AccountIdDTO accountId)
        {
            ServiceResponse<IEnumerable<TransactionDTO>> response = new ServiceResponse<IEnumerable<TransactionDTO>>();
            IEnumerable<TransactionDTO> transactions = (IEnumerable<TransactionDTO>)await _context.Transactions.FirstOrDefaultAsync(a => a.Id == accountId.Id);
            response.Data = transactions;
            return response;
        }

        public async Task<ServiceResponse<TransactionDTO>> UpdateTransaction(TransactionDTO transaction)
        {
            ServiceResponse<TransactionDTO> response = new ServiceResponse<TransactionDTO>();
            Transaction _transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);
            _transaction.Amount = (int)transaction.Amount;
            _transaction.TransactionType = transaction.TransactionType;
            _transaction.Date = transaction.Date;
            await _context.SaveChangesAsync();
            response.Data = transaction;
            return response;
        }

        public async Task<ServiceResponse<Transaction>> DeleteTransaction(TransactionIdDTO transactionId)
        {
            ServiceResponse<Transaction> response = new ServiceResponse<Transaction>();
            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transactionId.Id);
            response.Data = transaction;
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return response;
        }
    }
}
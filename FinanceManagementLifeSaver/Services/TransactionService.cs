using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        public async Task<ServiceResponse<TransactionDTO>> GetTransactionById(int transactionId)
        {
            ServiceResponse<TransactionDTO> response = new ServiceResponse<TransactionDTO>();
            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
            response.Data = _mapper.Map<Transaction, TransactionDTO>(transaction);
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<Transaction>>> GetTransactionsByAccountId(int accountId)
        {
            ServiceResponse<IEnumerable<Transaction>> response = new ServiceResponse<IEnumerable<Transaction>>();
            List<Transaction> transactions = (List<Transaction>)await _context.Transactions.Where(a => a.Account.Id == accountId).ToListAsync();
            if (!transactions.Any())
            {
                response.Success = false;
                return response;
            }
            response.Data = transactions;
            return response;
        }

        public async Task<ServiceResponse<Transaction>> UpdateTransaction(TransactionDTO transaction)
        {
            ServiceResponse<Transaction> response = new ServiceResponse<Transaction>();
            Transaction _transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transaction.Id);
            if (_transaction == null) {
                response.Success = false;
                return response;
            }
            _transaction.Amount = (int)transaction.Amount;
            _transaction.TransactionType = transaction.TransactionType;
            _transaction.Date = transaction.Date;
            _transaction.Description = transaction.Description;
            await _context.SaveChangesAsync();
            response.Data = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);
            return response;
        }

        public async Task<ServiceResponse<Transaction>> DeleteTransaction(TransactionIdDTO transactionId)
        {
            ServiceResponse<Transaction> response = new ServiceResponse<Transaction>();
            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transactionId.Id);
            if (transaction == null)
            {
                response.Success = false;
                return response;
            }
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            response.Data = transaction;
            return response;
        }
    }
}
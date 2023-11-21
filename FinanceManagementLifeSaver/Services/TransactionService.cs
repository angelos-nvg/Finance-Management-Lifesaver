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
using FinanceManagementLifesaver.Validations;
using FluentValidation;
using System;

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

            //Validations
            TransactionValidations validator = new TransactionValidations();
            var result = validator.Validate(transaction, options =>
            {
                options.IncludeRuleSets("Enums", "Dates", "Description", "Money", "Accounts");
            });
            response.Message = ValidationResponse.GetValidatorResponse(result.IsValid, result.Errors);
            if (response.Message != "")
            {
                response.Success = false;
                return response;
            }
            Models.Account account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == transaction.AccountId);
            if (account == null)
            {
                response.Success = false;
                response.Message = "Account not found";
                return response;
            }
            //Hotfix
            Transaction _transaction = new Transaction {
                Amount = (int)transaction.Amount,
                Description = transaction.Description,
                TransactionType = transaction.TransactionType,
                Account = account
            };
            await _context.Transactions.AddAsync(_transaction);
            await _context.SaveChangesAsync();
            transaction.Id = _transaction.Id;
            response.Data = transaction;
            return response;
        }

        public async Task<ServiceResponse<TransactionDTO>> GetTransactionById(int transactionId)
        {
            ServiceResponse<TransactionDTO> response = new ServiceResponse<TransactionDTO>();
            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
            if (transaction == null)
            {
                response.Success = false;
                response.Message = "Transaction not Found";
                return response;
            }
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

        public async Task<ServiceResponse<TransactionSaveDTO>> UpdateTransaction(TransactionSaveDTO transaction)
        {
            ServiceResponse<TransactionSaveDTO> response = new ServiceResponse<TransactionSaveDTO>();
            Transaction _transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transaction.Id);
            if (_transaction == null) {
                response.Success = false;
                return response;
            }
            Models.Account account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == transaction.AccountId);
            if (account == null)
            {
                response.Success = false;
                response.Message = "Account not found";
                return response;
            }
            _transaction.Amount = transaction.Amount;
            _transaction.TransactionType = transaction.TransactionType;
            _transaction.Date = transaction.Date;
            _transaction.Description = transaction.Description;
            _transaction.Account = account;
            //Validations
            TransactionValidations validator = new TransactionValidations();
            var result = validator.Validate(transaction, options =>
            {
                options.IncludeRuleSets("Enums", "Dates", "Description");
            });
            response.Message = ValidationResponse.GetValidatorResponse(result.IsValid, result.Errors);
            if (response.Message != "")
            {
                response.Success = false;
                return response;
            }

            await _context.SaveChangesAsync();
            response.Data = transaction;
            return response;
        }

        public async Task<ServiceResponse<TransactionDTO>> DeleteTransaction(TransactionIdDTO transactionId)
        {
            ServiceResponse<TransactionDTO> response = new ServiceResponse<TransactionDTO>();
            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(u => u.Id == transactionId.Id);
            if (transaction == null)
            {
                response.Success = false;
                return response;
            }
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            response.Success = true;
            return response;
        }
    }
}
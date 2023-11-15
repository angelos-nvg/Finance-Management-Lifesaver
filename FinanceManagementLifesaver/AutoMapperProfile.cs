using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.DTO.AccountDTO;
using FinanceManagementLifesaver.Models;
using System.Collections.Generic;

namespace FinanceManagementLifesaver
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            //CreateMap<AccountCreateDTO, Account>();
            //CreateMap<Account, AccountDTO>();
            //CreateMap<IEnumerable<Account>, IEnumerable<AccountDTO>>();
            //CreateMap<AccountDTO, Account>();
            CreateMap<Transaction, TransactionDTO>();
            //CreateMap<Account, AccountDTO>();
            CreateMap<TransactionDTO, Transaction>();
            //CreateMap<TransactionUpdateDTO, Transaction>();
            //CreateMap<Transaction, TransactionSaveDTO>();
            //CreateMap<TransactionSaveDTO, Transaction>();
        } 
    }
}

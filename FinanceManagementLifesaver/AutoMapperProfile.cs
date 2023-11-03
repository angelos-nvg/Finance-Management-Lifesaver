using AutoMapper;
using FinanaceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Models;
using System.Collections.Generic;

namespace FinanceManagementLifesaver
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<AccountSaveDTO, Account>();
            CreateMap<Account, AccountDTO>();
            CreateMap<IEnumerable<Account>, IEnumerable<AccountDTO>>();
            CreateMap<AccountDTO, Account>();
            CreateMap<Transaction, TransactionDTO>();
        } 
    }
}

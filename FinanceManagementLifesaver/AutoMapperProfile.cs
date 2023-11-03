using AutoMapper;
using FinanaceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Models;

namespace FinanceManagementLifesaver
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<AccountSaveDTO, Account>();
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();
        } 
    }
}

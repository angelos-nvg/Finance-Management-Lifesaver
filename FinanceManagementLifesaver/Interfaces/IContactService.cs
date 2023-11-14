using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IContactService
    {
        Task<ServiceResponse<ContactDTO>> CreateContact(ContactDTO contact);
        Task<ServiceResponse<ContactDTO>> GetContactById(int userId, int contactId);
        Task<ServiceResponse<IEnumerable<ContactDTO>>> GetContactsById(int Id);
        Task<ServiceResponse<ContactDTO>> UpdateContact(ContactDTO contact);
        Task<ServiceResponse<ContactDTO>> DeleteContact(int contactId);
    }
}

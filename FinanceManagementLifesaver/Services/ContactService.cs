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
using FinanceManagementLifesaver.Migrations;
using FinanceManagementLifesaver.Validations;
using FluentValidation;
using System;

namespace FinanceManagementLifesaver.Services
{
    public class ContactService : IContactService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContactService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ContactDTO>> CreateContact(ContactDTO contact)
        {
            ServiceResponse<ContactDTO> response = new ServiceResponse<ContactDTO>();
            var tempContact = await _context.Contacts.Where(c => c.Contact.Id == contact.Id || c.UserId == contact.Id);
            if(tempContact != null)
            {
                response.Success = false;
                response.Message = "Kontakt existiert bereits";
            }
            //Hotfix
            Contact _contact = new Contact
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email
            };
            await _context.Contacts.AddAsync(_contact);

            await _context.SaveChangesAsync();
            response.Data = contact;
            return response;
        }

        public async Task<ServiceResponse<ContactDTO>> GetContactById(int userId, int contactId)
        {
            ServiceResponse<ContactDTO> response = new ServiceResponse<ContactDTO>();
            Contact contact = await _context.Contacts.FirstOrDefaultAsync
                (c => c.UserId == contactId && c.ContactId == userId || 
                c.ContactId == contactId && c.UserId == userId);
            if (contact == null)
            {
                response.Success = false;
                response.Message = "Kontakt nicht gefunden";
            }
            response.Data = contact(contact);
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ContactDTO>>> GetContactsById(int accountId)
        {
            ServiceResponse<IEnumerable<ContactDTO>> response = new ServiceResponse<IEnumerable<ContactDTO>>();
            List<ContactDTO> contacts = (List<ContactDTO>)await _context.Contacts.Where(c => c.Contact.Id == accountId || c.UserId == accountId).ToListAsync();
            if (!contacts.Any())
            {
                response.Success = false;
                return response;
            }
            response.Data = contacts;
            return response;
        }

        public async Task<ServiceResponse<ContactDTO>> UpdateContact(ContactDTO contact)
        {
            ServiceResponse<ContactDTO> response = new ServiceResponse<ContactDTO>();
            Contact _contact = await _context.Contacts.FirstOrDefaultAsync(c => c.UserId == contact.Id || c.ContactId == contact.Id);
            if (_contact == null)
            {
                response.Success = false;
                response.Message = "Kontakt nicht gefunden";
                return response;
            }

            await _context.SaveChangesAsync();
            response.Data = contact;
            return response;
        }

        public async Task<ServiceResponse<ContactDTO>> DeleteContact(int contactId)
        {
            ServiceResponse<ContactDTO> response = new ServiceResponse<ContactDTO>();
            Contact contact = await _context.Contacts.FirstOrDefaultAsync(c => c.UserId == contactId || c.ContactId == contactId);
            if (contact == null)
            {
                response.Success = false;
                return response;
            }
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            response.Success = true;
            return response;
        }
    }
}
//using FinanceManagementLifesaver.DTO;
//using FinanceManagementLifesaver.DTO.AccountDTO;
//using FinanceManagementLifesaver.Interfaces;
//using FinanceManagementLifesaver.Models;
//using FinanceManagementLifesaver.ServiceResponse;
//using FinanceManagementLifesaver.Services;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FinanceManagementLifesaver.Controllers
//{
//    [EnableCors]
//    [Route("api/[contact]")]
//    [ApiController]
//    public class ContactController : ControllerBase
//    {
//        private readonly IContactService _contactService;

//        public ContactController(IContactService contactService)
//        {
//            _contactService = contactService;
//        }
//        [HttpGet("ContactsByAccount/{accountId}")]
//        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContactsById(int Id)
//        {
//            ServiceResponse<IEnumerable<ContactDTO>> response = await _contactService.GetContactsById(Id);
//            if (!response.Success)
//            {
//                return NotFound();
//            }
//            return Ok(response);
//        }
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ServiceResponse<ContactDTO>>> GetContactById(int userId, int contactId)
//        {
//            ServiceResponse<ContactDTO> response = await _contactService.GetContactById(userId, contactId);
//            if (!response.Success)
//            {
//                return NotFound();
//            }
//            return Ok(response);
//        }

//        [HttpPost]
//        public async Task<ActionResult<ServiceResponse<ContactDTO>>> Post(ContactDTO contact)
//        {
//            ServiceResponse<ContactDTO> response = await _contactService.CreateContact(contact);
//            if (!response.Success)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }

//        [HttpPut]
//        public async Task<ActionResult<ServiceResponse<ContactDTO>>> Put(ContactDTO contact)
//        {
//            ServiceResponse<ContactDTO> response = await _contactService.UpdateContact(contact);
//            if (!response.Success)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delete(ContactDTO contact)
//        {
//            ServiceResponse<ContactDTO> response = await _contactService.DeleteContact(contact.Id);
//            if (!response.Success)
//            {
//                return NotFound(response);
//            }
//            return Ok(response);
//        }
//    }
//}

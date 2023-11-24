using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.DTO.AccountDTO;
using FinanceManagementLifesaver.DTO.ToDoDTO;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using FinanceManagementLifesaver.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _todoService;

        public ToDoController(IToDoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetAllToDos()
        {
            ServiceResponse<IEnumerable<ToDo>> response = await _todoService.GetAllToDos();
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ToDoSaveDTO>>> Post(ToDoSaveDTO toDo)
        {
            ServiceResponse<ToDoSaveDTO> response = await _todoService.CreateToDo(toDo);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Created(Request.HttpContext.ToString(), response);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<ToDoSaveDTO>>> Put(ToDoSaveDTO todo)
        {
                ServiceResponse<ToDoSaveDTO> response = await _todoService.UpdateToDo(todo);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<ToDoIDDTO> response = await _todoService.DeleteToDos(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDoByUserId(int userId)
        {
            ServiceResponse<IEnumerable<ToDo>> response = await _todoService.GetToDoByUserId(userId);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpGet("ByDate/{Date}")]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetAllToDosByDate(DateTime enddate)
        {
            ServiceResponse<IEnumerable<ToDo>> response = await _todoService.GetAllToDosByDate(enddate);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}

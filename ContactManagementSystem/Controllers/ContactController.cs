using BusinessLogic.DTOs;
using BusinessLogic.Service.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContactsAsync()
        {
            var result = await _contactService.GetAllContactsAsync();
            return Ok(result);
        }

        [HttpGet("[Action]/{contactId}")]
        public async Task<IActionResult> GetContactByIdAsync(int contactId)
        {
            var result = await _contactService.GetContactByIdAsync(contactId);
            if(result.Result != null)
            {
                return Ok(result.Result);
            }
            if(result.NotFound)
            {
                return NotFound();
            }
            if (result.HasError)
            {
                return BadRequest(new { Message = result.ErrorMessage });
            }
            return BadRequest(new { Message = "UnExpected Error" });
        }

        [HttpPost]
        public async Task<IActionResult> AddNewContactAsync([FromBody]ContactDto dto)
        {
            var result = await _contactService.AddNewContactAsync(dto);
            if (result.Result != null)
            {
                return Ok(result.Result);
            }
            if (result.NotFound)
            {
                return NotFound();
            }
            if (result.HasError)
            {
                return BadRequest(new { Message = result.ErrorMessage });
            }
            return BadRequest(new { Message = "UnExpected Error" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactAsync(int id, ContactDto dto)
        {
            var result = await _contactService.UpdateContactAsync(id, dto);
            if (result.Result != null)
            {
                return Ok(result.Result);
            }
            if (result.NotFound)
            {
                return NotFound();
            }
            if (result.HasError)
            {
                return BadRequest(new { Message = result.ErrorMessage });
            }
            return BadRequest(new { Message = "UnExpected Error" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactByIdAsync(int id)
        {
            var result = await _contactService.DeleteContactByIdAsync(id);
            if (result.Result != null)
            {
                return Ok(result.Result);
            }
            if (result.NotFound)
            {
                return NotFound();
            }
            if (result.HasError)
            {
                return BadRequest(new { Message = result.ErrorMessage });
            }
            return BadRequest(new { Message = "UnExpected Error" });
        }
    }
}

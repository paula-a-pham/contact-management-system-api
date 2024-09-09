using BusinessLogic.DTOs;
using BusinessLogic.Service.Implementation;
using BusinessLogic.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactCategoryController : ControllerBase
    {
        private readonly IContactCategoryService _contactCategoryService;

        public ContactCategoryController(IContactCategoryService contactCategoryService)
        {
            _contactCategoryService = contactCategoryService;
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> GetAllContactCategoriesAsync()
        {
            var result = await _contactCategoryService.GetAllContactCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("[Action]/{contactId}")]
        public async Task<IActionResult> GetContactCategoryByIdAsync(int contactId)
        {
            var result = await _contactCategoryService.GetContactCategoryByIdAsync(contactId);
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

        [HttpPost("[Action]")]
        public async Task<IActionResult> AddNewContactCategoryAsync([FromBody] ContactCategoryDto dto)
        {
            var result = await _contactCategoryService.AddNewContactCategoryAsync(dto);
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

        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> UpdateContactCategoryAsync(int id, [FromBody] ContactCategoryDto dto)
        {
            var result = await _contactCategoryService.UpdateContactCategoryAsync(id, dto);
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

        [HttpDelete("[Action]/{id}")]
        public async Task<IActionResult> DeleteContactByIdAsync(int id)
        {
            var result = await _contactCategoryService.DeleteContactCategoryByIdAsync(id);
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

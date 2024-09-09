using BusinessLogic.DTOs;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interfaces
{
    public interface IContactCategoryService
    {
        Task<IEnumerable<ContactCategoryDto>> GetAllContactCategoriesAsync();
        Task<ResultDto> GetContactCategoryByIdAsync(int contactCatrgoryId);
        Task<ResultDto> AddNewContactCategoryAsync(ContactCategoryDto dto);
        Task<ResultDto> UpdateContactCategoryAsync(int id, ContactCategoryDto dto);
        Task<ResultDto> DeleteContactCategoryByIdAsync(int id);
    }
}

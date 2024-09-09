using BusinessLogic.DTOs;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAllContactsAsync();
        Task<ResultDto> GetContactByIdAsync(int contactId);
        Task<ResultDto> AddNewContactAsync(ContactDto dto);
        Task<ResultDto> UpdateContactAsync(int id, ContactDto dto);
        Task<ResultDto> DeleteContactByIdAsync(int id);
    }
}

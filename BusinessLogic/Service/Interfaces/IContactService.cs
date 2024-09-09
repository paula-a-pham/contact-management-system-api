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
        Task<ICollection<Contact>> GetAllContactsAsync();
        Task<ResultDto> GetContactById(int contactId);
    }
}

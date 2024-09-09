using BusinessLogic.DTOs;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class ContactDefinitionHelper
    {
        public ContactDefinitionHelper()
        {
            
        }

        public Contact DefineContact(ContactDto dto)
        {
            return new Contact
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                EmailAddress = dto.EmailAddress,
                Image = dto.Image,
                ContactCategoryId = dto.ContactCategoryId,
            };
        }
    }
}

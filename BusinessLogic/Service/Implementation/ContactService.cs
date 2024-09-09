using BusinessLogic.DTOs;
using BusinessLogic.Helpers;
using BusinessLogic.Service.Interfaces;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Implementation
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;
        private readonly ContactDefinitionHelper _contactDefinitionHelper;

        public ContactService(ApplicationDbContext context, ContactDefinitionHelper contactDefinitionHelper)
        {
            _context = context;
            _contactDefinitionHelper = contactDefinitionHelper;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _context.Contacts.Include(c => c.ContactCategory).ToListAsync();
        }

        public async Task<ResultDto> GetContactByIdAsync(int contactId)
        {
            var contact = await _context.Contacts.Where(c => c.ContactId == contactId).SingleOrDefaultAsync();
            if(contact != null)
            {
                return new ResultDto
                {
                    Result = contact,
                    NotFound = false,
                    HasError = false,
                };
            }
            return new ResultDto
            {
                NotFound = true,
                HasError = false,
            };
        }

        public async Task<ResultDto> AddNewContactAsync(ContactDto dto)
        {
            if(!dto.FirstName.IsNullOrEmpty() && !dto.PhoneNumber.IsNullOrEmpty())
            {
                try
                {
                    var existCheck = await _context.Contacts.AnyAsync(c => c.PhoneNumber == dto.PhoneNumber);
                    if (!existCheck)
                    {
                        Contact contact = _contactDefinitionHelper.DefineContact(dto);
                        await _context.Contacts.AddAsync(contact);
                        int saveResult = _context.SaveChanges();
                        if (saveResult > 0)
                        {
                            return new ResultDto
                            {
                                Result = contact,
                                NotFound = false,
                                HasError = false,
                            };
                        }
                        return new ResultDto
                        {
                            NotFound = false,
                            HasError = true,
                            ErrorMessage = "Contact Not Added.",
                        };
                    }
                    return new ResultDto
                    {
                        NotFound = false,
                        HasError = true,
                        ErrorMessage = "Phone Number Is Already Exist.",
                    };
                }
                catch (Exception ex)
                {
                    return new ResultDto
                    {
                        NotFound = false,
                        HasError = true,
                        ErrorMessage = ex.Message
                    };
                }
            }
            return new ResultDto
            {
                NotFound = false,
                HasError = true,
                ErrorMessage = "Empty Contact",
            };
        }

        public async Task<ResultDto> UpdateContactAsync(int id, ContactDto dto)
        {
            Contact? contact = await _context.Contacts.Where(c => c.ContactId == id).SingleOrDefaultAsync();
            if (contact != null)
            {
                try
                {
                    bool existCheck = await _context.Contacts.AnyAsync(c => c.PhoneNumber == dto.PhoneNumber);
                    if (!existCheck)
                    {
                        contact.FirstName = dto.FirstName;
                        contact.LastName = dto.LastName;
                        contact.PhoneNumber = dto.PhoneNumber;
                        contact.EmailAddress = dto.EmailAddress;
                        contact.Image = dto.Image;
                        contact.ContactCategoryId = dto.ContactCategoryId;
                        _context.Contacts.Update(contact);
                        int saveResult = _context.SaveChanges();
                        if (saveResult > 0)
                        {
                            return new ResultDto
                            {
                                Result = contact,
                                NotFound = false,
                                HasError = false,
                            };
                        }
                        return new ResultDto
                        {
                            NotFound = false,
                            HasError = true,
                            ErrorMessage = "Contact Not Updated.",
                        };
                    }
                    return new ResultDto
                    {
                        NotFound = false,
                        HasError = true,
                        ErrorMessage = "Phone Number Is Already Exist.",
                    };

                }
                catch (Exception ex)
                {
                    return new ResultDto
                    {
                        NotFound = false,
                        HasError = true,
                        ErrorMessage = ex.Message
                    };
                }
            }
            return new ResultDto
            {
                NotFound = true,
                HasError = false,
            };
        }

        public async Task<ResultDto> DeleteContactByIdAsync(int id)
        {
            Contact? contact = await _context.Contacts.Where(c => c.ContactId == id).SingleOrDefaultAsync();
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                int saveResult = _context.SaveChanges();
                if (saveResult > 0)
                {
                    return new ResultDto
                    {
                        Result = contact,
                        NotFound = false,
                        HasError = false,
                    };
                }
                return new ResultDto
                {
                    NotFound = false,
                    HasError = true,
                    ErrorMessage = "Contact Not Deleted.",
                };
            }
            return new ResultDto
            {
                NotFound = true,
                HasError = false,
            };
        }
    }
}

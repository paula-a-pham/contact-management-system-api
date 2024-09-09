using BusinessLogic.DTOs;
using BusinessLogic.Helpers;
using BusinessLogic.Service.Interfaces;
using Data.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Service.Implementation
{
    public class ContactCategoryService : IContactCategoryService
    {
        private readonly ApplicationDbContext _context;

        public ContactCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactCategoryDto>> GetAllContactCategoriesAsync()
        {
            return await _context.ContactCategories.Include(cc => cc.Contacts).Select(cc => new ContactCategoryDto
            {
                ContactCategoryId = cc.ContactCategoryId,
                Name = cc.Name,
                Contacts = cc.Contacts != null ? cc.Contacts.Select(ccc => new ContactDto
                {
                    ContactId = ccc.ContactId,
                    FirstName = ccc.FirstName,
                    LastName = ccc.LastName,
                    PhoneNumber = ccc.PhoneNumber,
                    EmailAddress = ccc.EmailAddress,
                    Image = ccc.Image,
                    ContactCategoryId = ccc.ContactCategoryId,
                    CategoryName = ccc.ContactCategory != null ? ccc.ContactCategory.Name : ""
                }).ToList() : new List<ContactDto>()
            }).ToListAsync();
        }

        public async Task<ResultDto> GetContactCategoryByIdAsync(int contactCatrgoryId)
        {
            var category = await _context.ContactCategories
                .Where(cc => cc.ContactCategoryId == contactCatrgoryId)
                .Include(cc => cc.Contacts)
                .Select(cc => new ContactCategoryDto
                {
                    ContactCategoryId = cc.ContactCategoryId,
                    Name = cc.Name,
                    Contacts = cc.Contacts != null ? cc.Contacts.Select(ccc => new ContactDto
                    {
                        ContactId = ccc.ContactId,
                        FirstName = ccc.FirstName,
                        LastName = ccc.LastName,
                        PhoneNumber = ccc.PhoneNumber,
                        EmailAddress = ccc.EmailAddress,
                        Image = ccc.Image,
                        ContactCategoryId = ccc.ContactCategoryId,
                        CategoryName = ccc.ContactCategory != null ? ccc.ContactCategory.Name : ""
                    }).ToList() : new List<ContactDto>()
                })
                .SingleOrDefaultAsync();
            if (category != null)
            {
                return new ResultDto
                {
                    Result = category,
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

        public async Task<ResultDto> AddNewContactCategoryAsync(ContactCategoryDto dto)
        {
            if (!dto.Name.IsNullOrEmpty())
            {
                try
                {
                    var existCheck = await _context.ContactCategories.AnyAsync(cc => cc.Name.ToLower() == dto.Name.ToLower());
                    if (!existCheck)
                    {
                        ContactCategory category = new ContactCategory { Name = dto.Name };
                        await _context.ContactCategories.AddAsync(category);
                        int saveResult = _context.SaveChanges();
                        if (saveResult > 0)
                        {
                            return new ResultDto
                            {
                                Result = category,
                                NotFound = false,
                                HasError = false,
                            };
                        }
                        return new ResultDto
                        {
                            NotFound = false,
                            HasError = true,
                            ErrorMessage = "Category Not Added.",
                        };
                    }
                    return new ResultDto
                    {
                        NotFound = false,
                        HasError = true,
                        ErrorMessage = "Category Is Already Exist.",
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
                ErrorMessage = "Empty Category Name",
            };
        }

        public async Task<ResultDto> UpdateContactCategoryAsync(int id, ContactCategoryDto dto)
        {
            ContactCategory? category = await _context.ContactCategories.Where(cc => cc.ContactCategoryId == id).SingleOrDefaultAsync();
            if (category != null)
            {
                try
                {
                    bool existCheck = await _context.ContactCategories.AnyAsync(cc => cc.Name == dto.Name);
                    if (!existCheck)
                    {
                        category.Name = dto.Name;                        
                        _context.ContactCategories.Update(category);
                        int saveResult = _context.SaveChanges();
                        if (saveResult > 0)
                        {
                            return new ResultDto
                            {
                                Result = category,
                                NotFound = false,
                                HasError = false,
                            };
                        }
                        return new ResultDto
                        {
                            NotFound = false,
                            HasError = true,
                            ErrorMessage = "Category Not Updated.",
                        };
                    }
                    return new ResultDto
                    {
                        NotFound = false,
                        HasError = true,
                        ErrorMessage = "Category Is Already Exist.",
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

        public async Task<ResultDto> DeleteContactCategoryByIdAsync(int id)
        {
            ContactCategory? category = await _context.ContactCategories.Where(cc => cc.ContactCategoryId == id).SingleOrDefaultAsync();
            if (category != null)
            {
                _context.ContactCategories.Remove(category);
                int saveResult = _context.SaveChanges();
                if (saveResult > 0)
                {
                    return new ResultDto
                    {
                        Result = category,
                        NotFound = false,
                        HasError = false,
                    };
                }
                return new ResultDto
                {
                    NotFound = false,
                    HasError = true,
                    ErrorMessage = "Category Not Deleted.",
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

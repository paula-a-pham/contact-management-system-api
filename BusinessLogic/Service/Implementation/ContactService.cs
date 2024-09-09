﻿using BusinessLogic.Service.Interfaces;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
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

        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Contact>> GetAllContactsAsync()
        {
            return await _context.Contacts.Include(c => c.ContactCategory).ToListAsync();
        }
    }
}

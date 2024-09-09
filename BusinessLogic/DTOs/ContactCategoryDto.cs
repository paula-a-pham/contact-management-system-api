using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class ContactCategoryDto
    {
        public int? ContactCategoryId { get; set; }
        public required string Name { get; set; }
        public ICollection<ContactDto>? Contacts { get; set; }
    }
}

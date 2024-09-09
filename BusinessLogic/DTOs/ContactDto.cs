using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class ContactDto
    {
        public int? ContactId { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Image { get; set; }
        public int? ContactCategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}

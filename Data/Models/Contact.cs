using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        [MaxLength(15)]
        public required string PhoneNumber { get; set; }
        [MaxLength(255)]
        public string? EmailAddress { get; set; }
        public string? Image { get; set; }
    }
}

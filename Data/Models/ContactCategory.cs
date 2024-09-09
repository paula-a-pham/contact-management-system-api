using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ContactCategory
    {
        public int ContactCategoryId { get; set; }
        [MaxLength(255)]
        public required string Name { get; set; }
    }
}

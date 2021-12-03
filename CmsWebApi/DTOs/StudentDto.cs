using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsWebApi.DTOs
{
    public class StudentDto
    {

        public int StudentId { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }

        
    }
}

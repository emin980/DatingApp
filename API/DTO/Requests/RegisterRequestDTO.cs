using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace API.DTO.Requests
{
    [Index(nameof(UserName), IsUnique = true)]
    public class RegisterRequestDTO{
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string EMailAdress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
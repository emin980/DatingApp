using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO.Requests
{
    public class ResetPasswordRequestDTO
    {
        public string UserName{get;set;}
        public string CurrentPassword{get;set;}
        public string NewPassword{get;set;}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webAPI.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
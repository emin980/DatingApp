using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webAPI.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserInfo user);
    }
}
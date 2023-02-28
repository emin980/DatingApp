using System.Security.Cryptography;
using System.Text;
using API.DTO.Requests;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Entities;

namespace API.Controllers.Base
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost()]
        public async Task<ActionResult<UserDTO>> Register(RegisterRequestDTO request)
        {
            if (await UserExists(request.UserName))
            {
                return BadRequest("Username is taken");
            }

            if (PasswordIsNotGood(request.Password))
            {
                return BadRequest("Password length must be bigger than 11 characters!.");
            }
            using var hmac = new HMACSHA512();
            var user = new UserInfo
            {
                UserName = request.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key,
                EmailAdress = request.EMailAdress,
                PhoneNumber = request.PhoneNumber

            };
            _context.UserInfo.Add(user);
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpPost()]
        public async Task<bool> UserExists(string username)
        {
            return await _context.UserInfo.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
        [HttpPost()]
        public bool PasswordIsNotGood(string Password)
        {
            return Password.Length < 8 ? true : false;
        }
        [HttpPost()]
        public async Task<ActionResult<UserDTO>> Login(LoginRequestDTO request)
        {
            var user = await _context.UserInfo
            .SingleOrDefaultAsync(x => x.UserName == request.UserName);
            if (user == null) return Unauthorized("Invalid Username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost()]
        public async Task<ActionResult<UserDTO>> ResetPassword(ResetPasswordRequestDTO request)
        {
            var user = await _context.UserInfo
            .SingleOrDefaultAsync(x => x.UserName == request.UserName);
            using var hmacNew = new HMACSHA512();
            if (user == null)
            {
                return Unauthorized("Invalid Username");
            }
            user.PasswordHash = hmacNew.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword));
            user.PasswordSalt = hmacNew.Key;
            _context.UserInfo.Update(user);
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
            // using var hmacCurrent=new HMACSHA512(user.PasswordSalt);
            // var computedCurrentHash=hmacCurrent.ComputeHash(Encoding.UTF8.GetBytes(request.CurrentPassword));
            // for(int i=0;i<computedCurrentHash.Length;i++){
            //     if(computedCurrentHash[i]!=user.PasswordHash[i]) return Unauthorized("Please enter your current password correctly!");
            // }
        }

        [HttpPost()]
        public async Task<ActionResult<UserDTO>> ChangePassword(ChangePasswordRequestDTO request)
        {
            var user = await _context.UserInfo.SingleOrDefaultAsync(x => x.UserName == request.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid Username");
            }
            using var hmacOld = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmacOld.ComputeHash(Encoding.UTF8.GetBytes(request.OldPassword));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }

            using var hmacNew = new HMACSHA512();
            user.PasswordHash = hmacNew.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword));
            user.PasswordSalt = hmacNew.Key;
            _context.UserInfo.Update(user);
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}
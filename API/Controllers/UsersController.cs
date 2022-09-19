using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        //--------------------------------------------------Sync----------------------------------------
        // [HttpGet]
        // public ActionResult<IEnumerable<AppUser>> GetUsers(){
        //     var usersList=_context.UserInfo.ToList();
        //     return usersList;
        // }
        // //api/Users/GetUser
        // [HttpGet("{Id}")]
        // public ActionResult<AppUser> GetUser(int id){
        //     return _context.UserInfo.Find(id);
        // }
        //--------------------------------------------------Async----------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUsers(){
            var usersList=_context.UserInfo.ToListAsync();
            return await usersList;
        }
        //api/Users/GetUser
        [HttpGet("{Id}")]
        public async Task<ActionResult<UserInfo>> GetUser(int id){
            return await _context.UserInfo.FindAsync(id);
        }
    }
}
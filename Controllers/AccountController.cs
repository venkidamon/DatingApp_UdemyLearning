using DatingApp.Data;
using DatingApp.Dtos;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenInterface _tokenService;

        public AccountController(DataContext context, ITokenInterface tokenService)
        {
            this._tokenService = tokenService;
            this._context = context;     
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (await UserExists(register.Username)) return BadRequest("User Already Taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser() {
                UserName = register.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var users = await _context.Users.SingleOrDefaultAsync(x => x.UserName == login.UserName);
            if(users == null)
            {
                return Unauthorized("Invalid UserName");
            }
            using var hmac = new HMACSHA512(users.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for(int i = 0; i < computedHash.Length; i++)
            {
                if (users.PasswordHash[i] != computedHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                UserName = login.UserName,
                Token = _tokenService.CreateToken(users)
            };
        }
    }
}

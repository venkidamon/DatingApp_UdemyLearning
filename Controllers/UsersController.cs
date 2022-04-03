using AutoMapper;
using DatingApp.Data;
using DatingApp.Dtos;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    [Authorize]
   
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepsository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepsository, IMapper mapper)
        {
            this._userRepsository = userRepsository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            //not optimized way
            /*var users = await _userRepsository.GetUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(usersToReturn);*/

            //optimized way
            var users = await _userRepsository.GetMembersAsync();
            return Ok(users);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            return Ok(await _userRepsository.GetUserByIdAsync(id));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>>  GetUser(string name)
        {
            var users = await _userRepsository.GetUserByUserNameAsync(name);
            return _mapper.Map<MemberDto>(users);
        }

    }
}

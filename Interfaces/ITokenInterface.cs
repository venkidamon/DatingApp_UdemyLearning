using DatingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Interfaces
{
    public interface ITokenInterface
    {
        string CreateToken(AppUser user);
    }
}

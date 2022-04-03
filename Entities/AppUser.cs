using DatingApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string KnownnAs { get; set; }
        public string Gender { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string LookingFor { get; set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }








        //the below code is causing error while adding automapper so we have created this method in extension folder as DateTime
       /* public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }*/
    }
}

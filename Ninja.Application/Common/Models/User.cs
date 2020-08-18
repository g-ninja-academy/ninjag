using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Models
{
    public class User
    {
        public User()
        {
        }

        public User(int id, string name, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
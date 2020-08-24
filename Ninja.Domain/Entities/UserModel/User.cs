using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Domain.Entities.UserModel
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
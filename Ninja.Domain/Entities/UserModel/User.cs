using System;
using System.Collections.Generic;
using System.Text;
using Ninja.Domain.Common;

namespace Ninja.Domain.Entities.UserModel
{
    [Collection("Users")]
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
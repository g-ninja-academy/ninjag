using System;
using System.Collections.Generic;
using System.Text;
using Ninja.Domain.Common;
using Ninja.Domain.Entities.AddressModel;

namespace Ninja.Domain.Entities.UserModel
{
    [Collection("Users")]
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string TelephoneNumber { get; set; }
        public int Age { get; set; }
        public List<Address> Address { get; set; }
    }
}
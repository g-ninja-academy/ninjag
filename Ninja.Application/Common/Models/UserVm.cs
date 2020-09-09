using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Models
{
    public class UserVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Lastname { get; set; }
        
        public string TelephoneNumber { get; set; }
        
        public int Age { get; set; }
        
        public List<AddressVm> Address { get; set; }
    }
}
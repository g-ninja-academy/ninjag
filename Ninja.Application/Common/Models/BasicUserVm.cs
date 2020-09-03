using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Common.Models
{
    public class BasicUserVm
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Lastname { get; set; }

        public string TelephoneNumber { get; set; }
        
        public int Age { get; set; }
        
        public List<AddressVM> Address { get; set; }
    }
}
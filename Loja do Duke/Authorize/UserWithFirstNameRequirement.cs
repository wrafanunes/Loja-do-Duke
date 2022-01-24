using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Authorize
{
    public class UserWithFirstNameRequirement : IAuthorizationRequirement
    {
        public string Name { get; set; }

        public UserWithFirstNameRequirement(string name)
        {
            Name = name;
        }
    }
}

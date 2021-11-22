using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Authorize
{
    public class AdminWithMoreThan1000DaysRequirement : IAuthorizationRequirement
    {
        public int Days { get; set; }

        public AdminWithMoreThan1000DaysRequirement(int days)
        {
            Days = days;
        }
    }
}

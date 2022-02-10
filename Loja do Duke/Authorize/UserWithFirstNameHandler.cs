using Loja_do_Duke.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Loja_do_Duke.Authorize
{
    public class UserWithFirstNameHandler : AuthorizationHandler<UserWithFirstNameRequirement>
    {
        public UserManager<IdentityUser> _manager { get; set; }
        public ApplicationDbContext _application { get; set; }

        public UserWithFirstNameHandler(UserManager<IdentityUser> manager, ApplicationDbContext application)
        {
            _manager = manager;
            _application = application;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserWithFirstNameRequirement requirement)
        {
            string userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (null != userId)
            {
                var user = _application.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
                var claims = Task.Run(async () => await _manager.GetClaimsAsync(user)).Result;
                var claim = claims.FirstOrDefault(c => c.Type == "PrimeiroNome");
                if (null != claim)
                {
                    if (claim.Value.ToLower().Contains(requirement.Name.ToLower()))
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}

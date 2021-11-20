using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Models.ViewModels
{
    public class UserClaimsVM
    {
        public string UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
        public UserClaimsVM()
        {
            Claims = new List<UserClaim>();
        }
    }
    
    public class UserClaim
    {
        public string Type { get; set; }
        public bool IsSelected { get; set; }
    }
}

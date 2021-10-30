using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Models.ViewModels
{
    public class TwoFactorAuthenticationVM
    {
        public string Code { get; set; }
        public string Token { get; set; }
        public string QRCodeUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Loja_do_Duke.Data
{
    public static class ClaimStore
    {
        public static List<Claim> claims = new List<Claim>()
        {
            new Claim("Criar","Create"),
            new Claim("Editar","Edit"),
            new Claim("Excluir","Delete")
        };
    }
}

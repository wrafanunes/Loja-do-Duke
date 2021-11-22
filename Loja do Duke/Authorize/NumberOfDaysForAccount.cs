using Loja_do_Duke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Authorize
{
    public class NumberOfDaysForAccount : INumberOfDaysForAccount
    {
        private readonly ApplicationDbContext _context;

        public NumberOfDaysForAccount(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Get(string userId)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            //no banco de dados o valor null em DateCreated é automaticamente reconhecido como o valor mínimo de DateTime
            if(null != user && user.DateCreated != DateTime.MinValue)
            {
                return (DateTime.Today - user.DateCreated).Days;
            }
            return 0;
        }
    }
}

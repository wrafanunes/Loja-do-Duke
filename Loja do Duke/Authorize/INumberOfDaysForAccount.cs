using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Authorize
{
    public interface INumberOfDaysForAccount
    {
        int Get(string userId);
    }
}

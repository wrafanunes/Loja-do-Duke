using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Models.ViewModels
{
    public class SupplyVM
    {
        public Supply Supply { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}

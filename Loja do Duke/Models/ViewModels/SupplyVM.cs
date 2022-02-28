using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Models.ViewModels
{
    public class SupplyVM
    {
        public Supply Supply { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
        public ApplicationUserSupply ApplicationUserSupply { get; set; }

        [DisplayName("Quantidade de Lei")]
        public int Lei { get; set; }

        [DisplayName("Capacidade do Inventário")]
        public short InventoryCapacity { get; set; }

        [DisplayName("Quantidade")]
        public int Quantity { get; set; }

        [DisplayName("Valor Total")]
        public int TotalValue { get; set; }
    }
}

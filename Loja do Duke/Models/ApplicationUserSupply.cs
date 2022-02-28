using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Models
{
    public class ApplicationUserSupply
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? SupplyId { get; set; }

        [DisplayName("Quantidade no inventário")]
        public int UserInventoryQuantity { get; set; }

        public ApplicationUserSupply()
        {

        }

        public ApplicationUserSupply(string userId, int? supplyId)
        {
            UserId = userId;
            SupplyId = supplyId;
        }
    }
}

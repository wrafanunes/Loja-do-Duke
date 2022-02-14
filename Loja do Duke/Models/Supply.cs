using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Models
{
    public class Supply
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Name { get; set; }

        [DisplayName("Preço")]
        [Required(ErrorMessage = "O campo Preço é obrigatório")]
        public int Price { get; set; }

        [DisplayName("Quantidade no inventário")]
        [Required(ErrorMessage = "O campo 'Quantidade no inventário' é obrigatório")]
        public int InventoryQuantity { get; set; }

        [DisplayName("Quantidade Disponível")]
        public int? AvailableQuantity { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        public string Description { get; set; }

        [DisplayName("Espaço necessário no inventário")]
        public short InventoryOccupation { get; set; }

        [DisplayName("Categoria")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}

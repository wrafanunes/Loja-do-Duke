using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Sequência de Exibição")]
        [Required(ErrorMessage = "O campo Tipo de Aplicação é obrigatório"), Range(1, short.MaxValue, ErrorMessage = "A sequência de exibição deve ser maior que 0.")]
        public short DisplayOrder { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As senhas digitadas divergem.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
        [Display(Name = "Nível de Autorização")]
        public string SelectedRole { get; set; }
    }
}

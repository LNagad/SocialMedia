using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.UserVM
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe introducir el nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe introducir una contrasena")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

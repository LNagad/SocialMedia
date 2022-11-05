using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.UserVM
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar su nombre")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Debe colocar su apellido")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un numero te telefono")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [RegularExpression(@"(809|829|849)\d{3}\d{4}", ErrorMessage = "Tu numero de telefono es invalido (FORMATO RD)")]
        public string? Phone { get; set; }

        public string? ProfileImg { get; set; }

        [Required(ErrorMessage = "Debe colocar un email")]
        public string? Email { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Debe colocar una password")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Debe colocar una password")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Compare(nameof(Password), ErrorMessage = "Las password no coinciden")]
        public string ConfirmPassword { get; set; }

        public string? ActivationKey { get; set; }

        [Required(ErrorMessage = "Debe colocar su foto de perfil")]
        [DataType(DataType.Upload)]
        public IFormFile File1 { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Eposta gerekli.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Şifre gerekli")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifreyi Onaylayın.")]
        [Compare("Password", ErrorMessage = "Şifre ve onay şifresi eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}

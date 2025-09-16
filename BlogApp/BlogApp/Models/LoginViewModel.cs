// LoginViewModel: Kullanıcı giriş formu için doğrulama anotasyonlarıyla kullanılan model.
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginViewModel
    {
        [Required] // Zorunlu alan
        [EmailAddress] // E-posta format kontrolü
        [Display(Name = "Eposta")]
        public string? Email { get; set;}

        [Required]  // Zorunlu alan
        [StringLength(10, ErrorMessage = "{0} alanı en  az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)] // 6-10 karakter arası
        [DataType(DataType.Password)] // Şifre giriş alanı olarak işaretle
        [Display(Name = "Parola")]
        public string? Password { get; set; }
    }
}
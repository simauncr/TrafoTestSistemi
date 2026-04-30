using System.ComponentModel.DataAnnotations;

namespace TrafoTestSistemi.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Mevcut şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mevcut Şifre")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Yeni şifre en az 4 karakter olmalıdır.")]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Yeni şifre ile tekrar alanı eşleşmiyor.")]
        [Display(Name = "Yeni Şifre Tekrar")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
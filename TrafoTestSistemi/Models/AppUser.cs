using System.ComponentModel.DataAnnotations;

namespace TrafoTestSistemi.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? NameSurname { get; set; }
    }
}
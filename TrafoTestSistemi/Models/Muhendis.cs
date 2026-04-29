using System.ComponentModel.DataAnnotations;

namespace TrafoTestSistemi.Models
{
    public class Muhendis
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; } = string.Empty;
    }
}

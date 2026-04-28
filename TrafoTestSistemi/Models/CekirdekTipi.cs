using System.ComponentModel.DataAnnotations;

namespace TrafoTestSistemi.Models
{
    public class CekirdekTipi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ad { get; set; } = string.Empty;
    }
}

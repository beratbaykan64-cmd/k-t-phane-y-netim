using System.ComponentModel.DataAnnotations;

namespace KutuphaneYonetim.Models
{
    public class Kitap
    {
        [Key]
        public int Id { get; set; } // PK

        [Required(ErrorMessage = "Kitap adı zorunludur.")]
        [Display(Name = "Kitap Adı")]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yazar adı zorunludur.")]
        public string Yazar { get; set; } = string.Empty;

        [Display(Name = "Sayfa Sayısı")]
        public int SayfaSayisi { get; set; }

        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
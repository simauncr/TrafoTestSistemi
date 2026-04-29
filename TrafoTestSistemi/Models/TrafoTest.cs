using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrafoTestSistemi.Models
{
    public class TrafoTest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProjeAdi { get; set; } = string.Empty;

        [Required]
        public string TasarimNo { get; set; } = string.Empty;

        [Required]
        public string Musteri { get; set; } = string.Empty;

        public string? DizaynId { get; set; }

        public int ElektrikMuhendisiId { get; set; }
        public Muhendis? ElektrikMuhendisi { get; set; }

        public int MekanikMuhendisiId { get; set; }
        public Muhendis? MekanikMuhendisi { get; set; }

        [NotMapped]
        [Required]
        [Display(Name = "Elek. Müh.")]
        public string ElektrikMuhendisiAdSoyad { get; set; } = string.Empty;

        [NotMapped]
        [Required]
        [Display(Name = "Mek. Müh.")]
        public string MekanikMuhendisiAdSoyad { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DizaynTarihi { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime TestTarihi { get; set; } = DateTime.Now;

        [Required]
        public double Guc { get; set; }

        [Required]
        public double GerilimYG { get; set; }

        [Required]
        public double GerilimAG { get; set; }

        [Required]
        public string? BaglantiGrubu { get; set; }

        [Required]
        public int Frekans { get; set; } = 50;

        [Required]
        public int CekirdekTipiId { get; set; }
        public CekirdekTipi? CekirdekTipi { get; set; }

        [Required]
        public int SacCinsiId { get; set; }
        public SacCinsi? SacCinsi { get; set; }

        [Required]
        public int KazanCinsiId { get; set; }
        public KazanCinsi? KazanCinsi { get; set; }

        [Required]
        public int YagCinsiId { get; set; }
        public YagCinsi? YagCinsi { get; set; }

        public double AG_IcCap_K_Hesap { get; set; }
        public double AG_IcCap_U_Hesap { get; set; }
        public double AG_IcCap_K_Test { get; set; }
        public double AG_IcCap_U_Test { get; set; }
        public double AG_DisCap_K_Hesap { get; set; }
        public double AG_DisCap_U_Hesap { get; set; }
        public double AG_DisCap_K_Test { get; set; }
        public double AG_DisCap_U_Test { get; set; }

        public double YG_IcCap_K_Hesap { get; set; }
        public double YG_IcCap_U_Hesap { get; set; }
        public double YG_IcCap_K_Test { get; set; }
        public double YG_IcCap_U_Test { get; set; }
        public double YG_DisCap_K_Hesap { get; set; }
        public double YG_DisCap_U_Hesap { get; set; }
        public double YG_DisCap_K_Test { get; set; }
        public double YG_DisCap_U_Test { get; set; }

        public double AG_Sapma_IcCap_K { get; set; }
        public double AG_Sapma_IcCap_U { get; set; }
        public double AG_Sapma_Radyal_K { get; set; }
        public double AG_Sapma_Radyal_U { get; set; }
        public double AG_Sapma_DisCap_K { get; set; }
        public double AG_Sapma_DisCap_U { get; set; }

        public double YG_Sapma_IcCap_K { get; set; }
        public double YG_Sapma_IcCap_U { get; set; }
        public double YG_Sapma_Radyal_K { get; set; }
        public double YG_Sapma_Radyal_U { get; set; }
        public double YG_Sapma_DisCap_K { get; set; }
        public double YG_Sapma_DisCap_U { get; set; }

        public double Cekirdek_Hesap { get; set; }
        public double Cekirdek_Test { get; set; }
        public double Cekirdek_Sapma { get; set; }
        public double AGIletken_Hesap { get; set; }
        public double AGIletken_Test { get; set; }
        public double AGIletken_Sapma { get; set; }
        public double YGIletken_Hesap { get; set; }
        public double YGIletken_Test { get; set; }
        public double YGIletken_Sapma { get; set; }
        public double Yag_Hesap { get; set; }
        public double Yag_Test { get; set; }
        public double Yag_Sapma { get; set; }

        public double P0_Garanti { get; set; }
        public double P0_Tolerans { get; set; }
        public double P0_Hesap { get; set; }
        public double P0_Test { get; set; }
        public double P0_Sapma_GH { get; set; }
        public double P0_Sapma_GT { get; set; }
        public double P0_Sapma_HT { get; set; }

        public double Pk_Garanti { get; set; }
        public double Pk_Tolerans { get; set; }
        public double Pk_Hesap { get; set; }
        public double Pk_Test { get; set; }
        public double Pk_Sapma_GH { get; set; }
        public double Pk_Sapma_GT { get; set; }
        public double Pk_Sapma_HT { get; set; }

        public double Uk_Garanti { get; set; }
        public double Uk_Tolerans { get; set; }
        public double Uk_Hesap { get; set; }
        public double Uk_Test { get; set; }
        public double Uk_Sapma_GH { get; set; }
        public double Uk_Sapma_GT { get; set; }
        public double Uk_Sapma_HT { get; set; }

        public double AG_Grad_Garanti { get; set; }
        public double AG_Grad_Hesap { get; set; }
        public double AG_Grad_Test { get; set; }
        public double AG_Grad_Sapma_GH { get; set; }
        public double AG_Grad_Sapma_GT { get; set; }
        public double AG_Grad_Sapma_HT { get; set; }

        public double YG_Grad_Garanti { get; set; }
        public double YG_Grad_Hesap { get; set; }
        public double YG_Grad_Test { get; set; }
        public double YG_Grad_Sapma_GH { get; set; }
        public double YG_Grad_Sapma_GT { get; set; }
        public double YG_Grad_Sapma_HT { get; set; }

        public double AG_SargiIsinma_Garanti { get; set; }
        public double AG_SargiIsinma_Hesap { get; set; }
        public double AG_SargiIsinma_Test { get; set; }
        public double AG_SargiIsinma_Sapma_GH { get; set; }
        public double AG_SargiIsinma_Sapma_GT { get; set; }
        public double AG_SargiIsinma_Sapma_HT { get; set; }

        public double YG_SargiIsinma_Garanti { get; set; }
        public double YG_SargiIsinma_Hesap { get; set; }
        public double YG_SargiIsinma_Test { get; set; }
        public double YG_SargiIsinma_Sapma_GH { get; set; }
        public double YG_SargiIsinma_Sapma_GT { get; set; }
        public double YG_SargiIsinma_Sapma_HT { get; set; }

        public double P55_ElekGaran { get; set; }
        public double P55_MekHesap { get; set; }
        public double P55_Test { get; set; }
        public double P55_Sapma_EGH { get; set; }
        public double P55_Sapma_MGT { get; set; }
        public double P55_Sapma_MHT { get; set; }

        public string Sonuc { get; set; } = "BEKLEMEDE";

        public void Hesapla()
        {
            AG_Sapma_IcCap_K = Math.Round(AG_IcCap_K_Test - AG_IcCap_K_Hesap, 2);
            AG_Sapma_IcCap_U = Math.Round(AG_IcCap_U_Test - AG_IcCap_U_Hesap, 2);
            AG_Sapma_DisCap_K = Math.Round(AG_DisCap_K_Test - AG_DisCap_K_Hesap, 2);
            AG_Sapma_DisCap_U = Math.Round(AG_DisCap_U_Test - AG_DisCap_U_Hesap, 2);
            AG_Sapma_Radyal_K = Math.Round((AG_DisCap_K_Test - AG_IcCap_K_Test) - (AG_DisCap_K_Hesap - AG_IcCap_K_Hesap), 2);
            AG_Sapma_Radyal_U = Math.Round((AG_DisCap_U_Test - AG_IcCap_U_Test) - (AG_DisCap_U_Hesap - AG_IcCap_U_Hesap), 2);

            YG_Sapma_IcCap_K = Math.Round(YG_IcCap_K_Test - YG_IcCap_K_Hesap, 2);
            YG_Sapma_IcCap_U = Math.Round(YG_IcCap_U_Test - YG_IcCap_U_Hesap, 2);
            YG_Sapma_DisCap_K = Math.Round(YG_DisCap_K_Test - YG_DisCap_K_Hesap, 2);
            YG_Sapma_DisCap_U = Math.Round(YG_DisCap_U_Test - YG_DisCap_U_Hesap, 2);
            YG_Sapma_Radyal_K = Math.Round((YG_DisCap_K_Test - YG_IcCap_K_Test) - (YG_DisCap_K_Hesap - YG_IcCap_K_Hesap), 2);
            YG_Sapma_Radyal_U = Math.Round((YG_DisCap_U_Test - YG_IcCap_U_Test) - (YG_DisCap_U_Hesap - YG_IcCap_U_Hesap), 2);

            Cekirdek_Sapma = Math.Round(Cekirdek_Test - Cekirdek_Hesap, 2);
            AGIletken_Sapma = Math.Round(AGIletken_Test - AGIletken_Hesap, 2);
            YGIletken_Sapma = Math.Round(YGIletken_Test - YGIletken_Hesap, 2);
            Yag_Sapma = Math.Round(Yag_Test - Yag_Hesap, 2);

            P0_Sapma_GH = P0_Garanti != 0 ? Math.Round((P0_Hesap - P0_Garanti) / P0_Garanti * 100, 2) : 0;
            P0_Sapma_GT = P0_Garanti != 0 ? Math.Round((P0_Test - P0_Garanti) / P0_Garanti * 100, 2) : 0;
            P0_Sapma_HT = P0_Hesap != 0 ? Math.Round((P0_Test - P0_Hesap) / P0_Hesap * 100, 2) : 0;

            Pk_Sapma_GH = Pk_Garanti != 0 ? Math.Round((Pk_Hesap - Pk_Garanti) / Pk_Garanti * 100, 2) : 0;
            Pk_Sapma_GT = Pk_Garanti != 0 ? Math.Round((Pk_Test - Pk_Garanti) / Pk_Garanti * 100, 2) : 0;
            Pk_Sapma_HT = Pk_Hesap != 0 ? Math.Round((Pk_Test - Pk_Hesap) / Pk_Hesap * 100, 2) : 0;

            Uk_Sapma_GH = Uk_Garanti != 0 ? Math.Round((Uk_Hesap - Uk_Garanti) / Uk_Garanti * 100, 2) : 0;
            Uk_Sapma_GT = Uk_Garanti != 0 ? Math.Round((Uk_Test - Uk_Garanti) / Uk_Garanti * 100, 2) : 0;
            Uk_Sapma_HT = Uk_Hesap != 0 ? Math.Round((Uk_Test - Uk_Hesap) / Uk_Hesap * 100, 2) : 0;

            AG_Grad_Sapma_GH = AG_Grad_Garanti != 0 ? Math.Round((AG_Grad_Hesap - AG_Grad_Garanti) / AG_Grad_Garanti * 100, 2) : 0;
            AG_Grad_Sapma_GT = AG_Grad_Garanti != 0 ? Math.Round((AG_Grad_Test - AG_Grad_Garanti) / AG_Grad_Garanti * 100, 2) : 0;
            AG_Grad_Sapma_HT = AG_Grad_Hesap != 0 ? Math.Round((AG_Grad_Test - AG_Grad_Hesap) / AG_Grad_Hesap * 100, 2) : 0;

            YG_Grad_Sapma_GH = YG_Grad_Garanti != 0 ? Math.Round((YG_Grad_Hesap - YG_Grad_Garanti) / YG_Grad_Garanti * 100, 2) : 0;
            YG_Grad_Sapma_GT = YG_Grad_Garanti != 0 ? Math.Round((YG_Grad_Test - YG_Grad_Garanti) / YG_Grad_Garanti * 100, 2) : 0;
            YG_Grad_Sapma_HT = YG_Grad_Hesap != 0 ? Math.Round((YG_Grad_Test - YG_Grad_Hesap) / YG_Grad_Hesap * 100, 2) : 0;

            AG_SargiIsinma_Sapma_GH = AG_SargiIsinma_Garanti != 0 ? Math.Round((AG_SargiIsinma_Hesap - AG_SargiIsinma_Garanti) / AG_SargiIsinma_Garanti * 100, 2) : 0;
            AG_SargiIsinma_Sapma_GT = AG_SargiIsinma_Garanti != 0 ? Math.Round((AG_SargiIsinma_Test - AG_SargiIsinma_Garanti) / AG_SargiIsinma_Garanti * 100, 2) : 0;
            AG_SargiIsinma_Sapma_HT = AG_SargiIsinma_Hesap != 0 ? Math.Round((AG_SargiIsinma_Test - AG_SargiIsinma_Hesap) / AG_SargiIsinma_Hesap * 100, 2) : 0;

            YG_SargiIsinma_Sapma_GH = YG_SargiIsinma_Garanti != 0 ? Math.Round((YG_SargiIsinma_Hesap - YG_SargiIsinma_Garanti) / YG_SargiIsinma_Garanti * 100, 2) : 0;
            YG_SargiIsinma_Sapma_GT = YG_SargiIsinma_Garanti != 0 ? Math.Round((YG_SargiIsinma_Test - YG_SargiIsinma_Garanti) / YG_SargiIsinma_Garanti * 100, 2) : 0;
            YG_SargiIsinma_Sapma_HT = YG_SargiIsinma_Hesap != 0 ? Math.Round((YG_SargiIsinma_Test - YG_SargiIsinma_Hesap) / YG_SargiIsinma_Hesap * 100, 2) : 0;

            P55_Sapma_EGH = P55_ElekGaran != 0 ? Math.Round((P55_MekHesap - P55_ElekGaran) / P55_ElekGaran * 100, 2) : 0;
            P55_Sapma_MGT = P55_ElekGaran != 0 ? Math.Round((P55_Test - P55_ElekGaran) / P55_ElekGaran * 100, 2) : 0;
            P55_Sapma_MHT = P55_MekHesap != 0 ? Math.Round((P55_Test - P55_MekHesap) / P55_MekHesap * 100, 2) : 0;

            bool hataVar = (Math.Abs(P0_Sapma_HT) > P0_Tolerans) ||
                           (Math.Abs(Pk_Sapma_HT) > Pk_Tolerans) ||
                           (Math.Abs(Uk_Sapma_HT) > Uk_Tolerans);

            Sonuc = hataVar ? "UYGUN DEĞİL" : "UYGUN";
        }
    }
}
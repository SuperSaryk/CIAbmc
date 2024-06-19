using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

namespace CIAbmc.Models
{
    public class Szablon
    {
        [Required]
        public string Opracowane_dla {  get; set; }
        [Required]
        public string Opracowane_przez {  get; set; }
        [Required]
        public DateOnly Data {  get; set; }
        [Required]
        [Key]
        public int Wersja { get; set; }
        public string Kluczowi_partnerzy {  get; set; }
        public string Kluczowe_aktywności { get; set; }
        public string Kluczowe_zasoby { get; set; }
        public string Propozycja_wartości { get; set; }
        public string Relacja_z_klientami { get; set; }
        public string Kanaly_dotarcia { get; set; }
        public string Segmenty_klientów { get; set; }
        public string Struktura_kosztow { get; set; }
        public string Strumienie_przychodów { get; set; }

    }
}

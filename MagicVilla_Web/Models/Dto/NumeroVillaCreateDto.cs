using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MagicVilla_Web.Models
{
    public class NumeroVillaCreateDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]  
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; }
    }
}

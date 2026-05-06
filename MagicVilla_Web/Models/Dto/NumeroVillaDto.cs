using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_Web.Models
{
    public class NumeroVillaDto
    {
        [Required]
        public int VillaNo { get; set; }

        [Required, ForeignKey("Villa")]
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; }

    }
}

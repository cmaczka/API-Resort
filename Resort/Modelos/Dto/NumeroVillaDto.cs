using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resort.Modelos.Dto
{
    public class NumeroVillaDto
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; }
        public VillaDto Villa { get; set; }

    }
}

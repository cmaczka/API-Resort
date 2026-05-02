using Resort.Modelos.Dto;

namespace Resort.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto { Id = 1, Nombre = "Villa 1", MetrosCuadrados=50, Ocupantes=3},
            new VillaDto { Id = 2, Nombre = "Villa 2", MetrosCuadrados=60, Ocupantes=4},
            new VillaDto { Id = 3, Nombre = "Villa 3", MetrosCuadrados=70, Ocupantes=5}
        };
    }
}

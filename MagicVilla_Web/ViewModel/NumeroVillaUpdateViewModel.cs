using MagicVilla_Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.ViewModel
{
    public class NumeroVillaUpdateViewModel
    {
        public NumeroVillaUpdateViewModel() 
        {
            NumeroVilla = new NumeroVillaUpdateDto();
            VillaList = new List<SelectListItem>();
        }
        public NumeroVillaUpdateDto NumeroVilla { get; set; }
         public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}

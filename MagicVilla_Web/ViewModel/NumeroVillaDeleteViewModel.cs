using MagicVilla_Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.ViewModel
{
    public class NumeroVillaDeleteViewModel
    {
        public NumeroVillaDeleteViewModel() 
        {
            NumeroVilla = new NumeroVillaDto();
            VillaList = new List<SelectListItem>();
        }
        public NumeroVillaDto NumeroVilla { get; set; }
         public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}

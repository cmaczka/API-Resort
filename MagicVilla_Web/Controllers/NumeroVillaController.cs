using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;
using MagicVilla_Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class NumeroVillaController : Controller
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IMapper _mapper;
        private readonly INumeroVillaService _numeroVillaService;
        private readonly IVillaService _villaService;
        public NumeroVillaController(INumeroVillaService numeroVillaService, IVillaService villaService, IMapper mapper, ILogger<NumeroVillaController> logger)
        {
            _numeroVillaService = numeroVillaService;
            _villaService = villaService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> IndexNumeroVilla()
        {
            List<NumeroVillaDto> numeroVillalist = new();
            var response = await _numeroVillaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsExitoso)
            {
                numeroVillalist = JsonConvert.DeserializeObject<List<NumeroVillaDto>>(Convert.ToString(response.Resultado));
            }
            return View(numeroVillalist);
        }
        public async Task<IActionResult> CreateNumeroVilla()
        {
            return View();
        }
        public async Task<IActionResult> CrearNumeroVilla(NumeroVillaViewModel model)
        {
            NumeroVillaViewModel numeroVillaVM = new();
            var response = await _villaService.GetAllAsync<APIResponse>();

            if(response != null && response.IsExitoso)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)).Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                });
            }
            //await _numeroVillaService.CreateAsync(model.NumeroVilla);

            return View(numeroVillaVM);
        }
    }
}

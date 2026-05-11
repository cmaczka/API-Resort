using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class NumeroVillaController : Controller
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IMapper _mapper;
        private readonly INumeroVillaService _numeroVillaService;
        public NumeroVillaController(INumeroVillaService numeroVillaService, IMapper mapper, ILogger<NumeroVillaController> logger)
        {
            _numeroVillaService = numeroVillaService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            List<NumeroVillaDto> numeroVillalist = new();
            var response = await _numeroVillaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsExitoso)
            {
                numeroVillalist = JsonConvert.DeserializeObject<List<NumeroVillaDto>>(Convert.ToString(response.Resultado));
            }
            return View(numeroVillalist);
        }
    }
}

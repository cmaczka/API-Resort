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
        //public async Task<IActionResult> CreateNumeroVilla()
        //{
        //    return View();
        //}
        public async Task<IActionResult> CrearNumeroVilla()
        {
            NumeroVillaViewModel numeroVillaVM = new();
            var response = await _villaService.GetAllAsync<APIResponse>();

            if (response != null && response.IsExitoso)
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

        [HttpPost]
        public async Task<IActionResult> CrearNumeroVilla(NumeroVillaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.CreateAsync<APIResponse>(model.NumeroVilla);
                if (response != null && response.IsExitoso)
                {
                    return RedirectToAction(nameof(IndexNumeroVilla));
                }
                else
                {
                    if (response.Errores.Count > 0)
                    {
                        foreach (var error in response.Errores)
                        {
                            ModelState.AddModelError("Error", error);
                        }
                    }
                }
            }
            var villaResponse = await _villaService.GetAllAsync<APIResponse>();
            if (villaResponse != null && villaResponse.IsExitoso)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(villaResponse.Resultado)).Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                });
            }
            return View(model);
        }

        public async Task<IActionResult> ActualizarNumeroVilla(int VillaNo)
        {
            var response = await _numeroVillaService.GetAsync<APIResponse>(VillaNo);
            if (response != null && response.IsExitoso)
            {
                NumeroVillaDto numeroVillaDto = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                NumeroVillaUpdateViewModel numeroVillaVM = new()
                {
                    NumeroVilla = _mapper.Map<NumeroVillaUpdateDto>(numeroVillaDto)
                };
                var villaResponse = await _villaService.GetAllAsync<APIResponse>();
                if (villaResponse != null && villaResponse.IsExitoso)
                {
                    numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(villaResponse.Resultado)).Select(i => new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.Id.ToString()
                    });
                }
                return View(numeroVillaVM);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarNumeroVilla(NumeroVillaUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.UpdateAsync<APIResponse>(model.NumeroVilla);
                if (response != null && response.IsExitoso)
                {
                    return RedirectToAction(nameof(IndexNumeroVilla));
                }
                else
                {
                    if (response.Errores.Count > 0)
                    {
                        foreach (var error in response.Errores)
                        {
                            ModelState.AddModelError("Error", error);
                        }
                    }
                }
            }
            var villaResponse = await _villaService.GetAllAsync<APIResponse>();
            if (villaResponse != null && villaResponse.IsExitoso)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(villaResponse.Resultado)).Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                });
            }
            return View(model);
        }
    }
}

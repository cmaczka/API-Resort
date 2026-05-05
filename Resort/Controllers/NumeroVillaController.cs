using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Resort.Datos;
using Resort.Modelos;
using Resort.Modelos.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Resort.Repositorio.IRepositorio;

namespace Resort.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly INumeroVillaRepositorio _numeroVillaRepo;
        private readonly IVillaRepositorio _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, INumeroVillaRepositorio numeroVillaRepo, IMapper mapper, IVillaRepositorio villaRepositorio)
        {
            _logger = logger;
            _mapper = mapper;
            _numeroVillaRepo = numeroVillaRepo;
            _villaRepo=villaRepositorio;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los números de villa");
                IEnumerable<NumeroVilla> numeroVillas = await _numeroVillaRepo.ObtenerTodos();
                IEnumerable<NumeroVillaDto> numeroVillaDtos = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillas);
                _response.Resultado = numeroVillaDtos;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "Ocurrió un error al obtener las villas" };
                _response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response); 
            }
           
        }
        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("El id de la villa no puede ser 0");
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { "El id de la villa no puede ser 0" };
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var numeroVilla = _numeroVillaRepo.Obtener(v => v.VillaId == id);
                if (numeroVilla == null)
                {
                    _logger.LogError($"El número de villa con id {id} no fue encontrado");
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { $"El número de villa con id {id} no fue encontrado" };
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                NumeroVillaDto numeroVillaDto = _mapper.Map<NumeroVillaDto>(numeroVilla);
                _response.Resultado = numeroVillaDto;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "Ocurrió un error al obtener el número de villa" };
                _response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
           
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNumeroVilla([FromBody] NumeroVillaCreateDto numeroVillaCreateDto)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    _response.IsExitoso = false;
                    _response.Errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (numeroVillaCreateDto == null)
                {
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { "El objeto villaCreateDto no puede ser null" };  
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if(await _numeroVillaRepo.Obtener(v => v.VillaNo == numeroVillaCreateDto.VillaNo) != null)
                {
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { $"El número de villa {numeroVillaCreateDto.VillaNo} ya existe" };
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _villaRepo.Obtener(v => v.Id == numeroVillaCreateDto.VillaId) == null)
                {
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { $"El número de villa {numeroVillaCreateDto.VillaNo} ya existe" };
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                NumeroVilla numeroVilla = _mapper.Map<NumeroVilla>(numeroVillaCreateDto);
                numeroVilla.FechaCreacion = DateTime.Now;
                numeroVilla.FechaModificacion = DateTime.Now;
                await _numeroVillaRepo.Crear(numeroVilla);
                _response.Resultado = numeroVilla;
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetNumeroVilla), new { id = numeroVilla.VillaId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "Ocurrió un error al crear el número de villa " + ex.ToString()};
                _response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            
        }
        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { "El id de la villa no puede ser 0" };
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;    
                    return BadRequest(_response);
                }
                var villa = await _numeroVillaRepo.Obtener(v => v.VillaId == id);
                if (villa == null)
                {
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { $"El número de villa con id {id} no fue encontrado" };
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _numeroVillaRepo.Eliminar(villa);
                _response.IsExitoso = true;
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "Ocurrió un error al eliminar la villa" };
                _response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
           
        }
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateNumeroVilla([FromBody] NumeroVillaUpdateDto numeroVillaDto)
        {
            if (ModelState.IsValid == false)
            {
                _response.IsExitoso = false;
                _response.Errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;    
                return BadRequest(_response);
            }
            if (numeroVillaDto == null )
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "El objeto villaDto no puede ser null" };
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (numeroVillaDto.VillaId == 0)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "El id de la villa no puede ser 0" };
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var numeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaId == numeroVillaDto.VillaId);
            if (numeroVilla == null)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { $"El número de villa con id {numeroVillaDto.VillaId} no fue encontrado" };
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            var villa = await _villaRepo.Obtener(v => v.Id == numeroVillaDto.VillaId);
            if (villa == null)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { $"La villa con id {numeroVillaDto.VillaId} no fue encontrada " };
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _mapper.Map(numeroVillaDto, villa);
            // ✅ establecer el valor original (clave)
            _numeroVillaRepo.SetOriginalRowVersion(numeroVilla, numeroVillaDto.RowVersion);

            try
            {
                await _numeroVillaRepo.Grabar();
            }
            catch (DbUpdateConcurrencyException)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "El número de villa fue modificado por otro usuario" };
                _response.StatusCode = System.Net.HttpStatusCode.Conflict;
                return Conflict(_response);
            }
            
            _response.IsExitoso = true;
            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            return Ok(_response);
        }
        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> UpdatePartialVilla(
    int id,
    [FromBody] JsonPatchDocument<VillaUpdateDto> patchDoc,
    [FromHeader(Name = "If-Match")] string rowVersionBase64)
        {
            if (patchDoc == null || id == 0)
                return BadRequest();

            var villa = await _numeroVillaRepo.Obtener(v => v.VillaId == id, false);

            if (villa == null)
                return NotFound();

            var villaUpdateDto = _mapper.Map<VillaUpdateDto>(villa);

            patchDoc.ApplyTo(villaUpdateDto, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ✅ convertir RowVersion
            byte[] originalRowVersion;
            try
            {
                originalRowVersion = Convert.FromBase64String(rowVersionBase64);
            }
            catch
            {
                return BadRequest("RowVersion inválido");
            }

            // ✅ establecer ORIGINAL
            _numeroVillaRepo.SetOriginalRowVersion(villa, originalRowVersion);

            // ✅ mapear cambios
            _mapper.Map(villaUpdateDto, villa);

            try
            {
                await _numeroVillaRepo.Actualizar(villa);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("El número de villa fue modificado por otro usuario");
            }

            return NoContent();
        }
    }
}
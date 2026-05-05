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
    public class ResortController : ControllerBase
    {
        private readonly ILogger<ResortController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public ResortController(ILogger<ResortController> logger, IVillaRepositorio villaRepo, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _villaRepo = villaRepo;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las villas");
                IEnumerable<Villa> villas = await _villaRepo.ObtenerTodos();
                IEnumerable<VillaDto> villaDtos = _mapper.Map<IEnumerable<VillaDto>>(villas);
                _response.Resultado = villaDtos;
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
        public ActionResult<APIResponse> GetVilla(int id)
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
                var villa = _villaRepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _logger.LogError($"La villa con id {id} no fue encontrada");
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { $"La villa con id {id} no fue encontrada" };
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                VillaDto villaDto = _mapper.Map<VillaDto>(villa);
                _response.Resultado = villaDto;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "Ocurrió un error al obtener la villa" };
                _response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
           
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
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

                if (villaCreateDto == null)
                {
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { "El objeto villaCreateDto no puede ser null" };  
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (_villaRepo.Obtener(v => v.Nombre.ToLower() == villaCreateDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe.");
                    return BadRequest(ModelState);
                }
                Villa villa = _mapper.Map<Villa>(villaCreateDto);
                villa.FechaCreacion = DateTime.Now;
                villa.FechaActualizacion = DateTime.Now;
                await _villaRepo.Crear(villa);
                _response.Resultado = villaCreateDto;
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetVilla), new { id = villa.Id }, _response);
            }
            catch (Exception)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "Ocurrió un error al crear la villa" };
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
                var villa = await _villaRepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _response.IsExitoso = false;
                    _response.Errores = new List<string> { $"La villa con id {id} no fue encontrada" };
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _villaRepo.Eliminar(villa);
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
        public async Task<ActionResult<APIResponse>> UpdateVilla([FromBody] VillaUpdateDto villaDto)
        {
            if (ModelState.IsValid == false)
            {
                _response.IsExitoso = false;
                _response.Errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;    
                return BadRequest(_response);
            }
            if (villaDto == null )
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "El objeto villaDto no puede ser null" };
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (villaDto.Id == 0)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "El id de la villa no puede ser 0" };
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var villa = await _villaRepo.Obtener(v => v.Id == villaDto.Id);
            if (villa == null)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { $"La villa con id {villaDto.Id} no fue encontrada" };
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _mapper.Map(villaDto, villa);

            // ✅ establecer el valor original (clave)
            _villaRepo.SetOriginalRowVersion(villa, villaDto.RowVersion);

            try
            {
                await _villaRepo.Grabar();
            }
            catch (DbUpdateConcurrencyException)
            {
                _response.IsExitoso = false;
                _response.Errores = new List<string> { "La villa fue modificada por otro usuario" };
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

            var villa = await _villaRepo.Obtener(v => v.Id == id, false);

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
            _villaRepo.SetOriginalRowVersion(villa, originalRowVersion);

            // ✅ mapear cambios
            _mapper.Map(villaUpdateDto, villa);

            try
            {
                await _villaRepo.Actualizar(villa);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("La villa fue modificada por otro usuario");
            }

            return NoContent();
        }
    }
}
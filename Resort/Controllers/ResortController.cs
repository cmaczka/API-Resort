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
        public ResortController(ILogger<ResortController> logger, IVillaRepositorio villaRepo, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _villaRepo = villaRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obteniendo todas las villas");
            IEnumerable<Villa> villas = await _villaRepo.ObtenerTodos();
            IEnumerable<VillaDto> villaDtos = _mapper.Map<IEnumerable<VillaDto>>(villas);
            return Ok(villaDtos);
        }
        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("El id de la villa no puede ser 0");
                return BadRequest();
            }
            var villa = _villaRepo.Obtener(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            VillaDto villaDto = _mapper.Map<VillaDto>(villa);
            return Ok(villaDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaCreateDto>> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (villaCreateDto == null)
            {
                return BadRequest();
            }
            
            if (_villaRepo.Obtener(v => v.Nombre.ToLower() == villaCreateDto.Nombre.ToLower())!=null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe.");
                return BadRequest(ModelState);
            }
             Villa villa = _mapper.Map<Villa>(villaCreateDto);
            await _villaRepo.Crear(villa);
            return CreatedAtAction(nameof(GetVilla), new { id = villa.Id }, villaCreateDto);
        }
        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _villaRepo.Obtener(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            await _villaRepo.Eliminar(villa);
            return NoContent();
        }
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateVilla([FromBody] VillaUpdateDto villaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            if (villaDto == null )
            {
                return BadRequest();
            }
            if (villaDto.Id == 0)
            {
                return BadRequest();
            }
            var villa = await _villaRepo.Obtener(v => v.Id == villaDto.Id);
            if (villa == null)
            {
                return NotFound();
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
                return Conflict("La villa fue modificada por otro usuario");
            }
            
            return NoContent();
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Resort.Datos;
using Resort.Modelos;
using Resort.Modelos.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Resort.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ResortController : ControllerBase
    {
        private readonly ILogger<ResortController> _logger;
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        public ResortController(ILogger<ResortController> logger, ApplicationDBContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obteniendo todas las villas");
            IEnumerable<Villa> villas = await _db.Villas.ToListAsync();
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
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
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
            
            if (_db.Villas.Any(v => v.Nombre.ToLower() == villaCreateDto.Nombre.ToLower()))
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe.");
                return BadRequest(ModelState);
            }
             Villa villa = _mapper.Map<Villa>(villaCreateDto);
            _db.Villas.Add(villa);
            await _db.SaveChangesAsync();
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
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            if (id == 0 || villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            _mapper.Map(villaDto, villa);
            _db.Entry(villa).Property(x => x.RowVersion).OriginalValue = villaDto.RowVersion;
            try
            {
                await _db.SaveChangesAsync();
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
        public async Task<ActionResult> UpdatePartialVilla(int id, [FromBody] JsonPatchDocument<VillaUpdateDto> patchDoc)
        {
            if (patchDoc == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
                     

            if (villa == null)
            {
                return NotFound();
            }
            VillaUpdateDto villaUpdateDto = _mapper.Map<VillaUpdateDto>(villa);
            patchDoc.ApplyTo(villaUpdateDto, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(villaUpdateDto,villa); 
            _db.Entry(villa).Property(x => x.RowVersion).OriginalValue = villaUpdateDto.RowVersion;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("La villa fue modificada por otro usuario");
            }
            return NoContent();
        }
    }
}
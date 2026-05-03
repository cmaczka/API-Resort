using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Resort.Datos;
using Resort.Modelos;
using Resort.Modelos.Dto;

namespace Resort.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ResortController : ControllerBase
    {
        private readonly ILogger<ResortController> _logger;
        private readonly ApplicationDBContext _db;
        public ResortController(ILogger<ResortController> logger, ApplicationDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obteniendo todas las villas");
            return Ok(_db.Villas.ToList());
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

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest();
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (_db.Villas.Any(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()))
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe.");
                return BadRequest(ModelState);
            }
            Villa villa = new Villa
            {
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                Tarifa = villaDto.Tarifa,
                Ocupantes = villaDto.Ocupantes,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                ImageUrl = villaDto.ImageUrl,
                Amenidad = villaDto.Amenidad
            };
            _db.Villas.Add(villa);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetVilla), new { id = villaDto.Id }, villaDto);
        }
        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteVilla(int id)
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
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            if (id == 0 || villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            Villa modelo = new() {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                Tarifa = villaDto.Tarifa,
                Ocupantes = villaDto.Ocupantes,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                ImageUrl = villaDto.ImageUrl,
                Amenidad = villaDto.Amenidad
            };
            _db.Villas.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdatePartialVilla(int id, [FromBody] JsonPatchDocument<VillaDto> patchDoc)
        {
            if (patchDoc == null || id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            VillaDto villaDto = new() {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                Tarifa = villa.Tarifa,
                Ocupantes = villa.Ocupantes,
                MetrosCuadrados = villa.MetrosCuadrados,
                ImageUrl = villa.ImageUrl,
                Amenidad = villa.Amenidad
            };

            if(villa == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(villaDto);
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
           
              Villa modelo = new() {
                 Id = villaDto.Id,
                 Nombre = villaDto.Nombre,
                 Detalle = villaDto.Detalle,
                 Tarifa = villaDto.Tarifa,
                 Ocupantes = villaDto.Ocupantes,
                 MetrosCuadrados = villaDto.MetrosCuadrados,
                 ImageUrl = villaDto.ImageUrl,
                 Amenidad = villaDto.Amenidad
              };

            _db.Villas.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
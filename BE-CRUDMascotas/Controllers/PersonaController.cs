using BE_CRUDMascotas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDMascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public PersonaController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

                var listMascotas = await _context.Persona.ToListAsync();
                return Ok(listMascotas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        
        //prueba
        [HttpGet("prueba")]
        public async Task<IActionResult> Gets()
        {
            try
            {
                var listMascotas = await _context.Persona.Where(
                    e => e.Nombre.StartsWith("P")).ToListAsync();
                return Ok(listMascotas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var mascota =await _context.Persona.FindAsync(id);
                if(mascota is null)
                {
                    return NotFound();
                }
                return Ok(mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var mascota = await _context.Persona.FindAsync(id);
                if (mascota is null)
                {
                    return NotFound();
                }
                _context.Persona.Remove(mascota);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public async Task<ActionResult> Post(Persona persona)
        {
            try
            {
               
                _context.Persona.Add(persona);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Get", new {id = persona.Id}, persona);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Persona persona)
        {
            try
            {
                if (id!=persona.Id)
                {
                    return BadRequest();
                }
                var personaItem = await _context.Persona.FindAsync(id);
                if (personaItem == null)
                {
                    return NotFound();
                }

                personaItem.Nombre= persona.Nombre;
                

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

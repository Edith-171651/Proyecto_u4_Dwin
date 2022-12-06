using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API_ESCUELA.Models;

namespace WEB_API_ESCUELA.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/profesor")]//----<--------ojo
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly ESCUELA_PROYECTOContext _context; //se la pasan las inyecciones de ependencia
        /*A través del contexto de datos, recibe el constructor y lo almacena en esta variable privada (_context)*/
        public ProfesorController(ESCUELA_PROYECTOContext context)
        {
            _context = context;  //se iguala la variable local por la pública
        }

        //CONSULTA SELECT * FROM
        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesor()
        {
            return await _context.Profesors.ToListAsync();
        }

        //CONSULTA SELECT * FROM WHERE ID = N
        // GET:api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesor(decimal id)
        {
            var profesor = await _context.Profesors.FindAsync(id);

            if (profesor == null)
            {
                return NotFound();
            }

            return profesor;
        }

        //A L T A S
        // POST: api/[controller]
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Profesor>> PostProfesor(Profesor profesor) //nombre del modelo
        {
            _context.Profesors.Add(profesor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfesor), new { Id = profesor.Id }, profesor);  //minuscula
        }


        //C A M B I O S
        //// PUT:api/[controller]/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesor(decimal id, Profesor profesor)  //Este metodo es para actualizar, está decorado con  [HttpPut("{id}")] 
        {
            if (id != profesor.Id)
            {
                return BadRequest();  //Si el id no es igual que lo que se pusó en xxx.ID regresa un  BadReuqest()
            }
            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();  //intenta salvar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent(); //si todo estuvo bien retorna un NoContect (), por eso vemos el 204.
        }

        //B A J A S 
        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Profesor>> DeleteProfesor(decimal id)
        {
            var profesor = await _context.Profesors.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            _context.Profesors.Remove(profesor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesorExists(decimal id)
        {
            return _context.Profesors.Any(e => e.Id == id);
        }
    }
}


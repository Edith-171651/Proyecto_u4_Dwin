using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API_ESCUELA.Models;


namespace WEB_API_ESCUELA.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/alumno")]//----<--------ojo
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly ESCUELA_PROYECTOContext _context; //se la pasan las inyecciones de ependencia
        /*A través del contexto de datos, recibe el constructor y lo almacena en esta variable privada (_context)*/
        public AlumnoController(ESCUELA_PROYECTOContext context)
        {
            _context = context;  //se iguala la variable local por la pública
        }

        //CONSULTA SELECT * FROM
        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumno()
        {
            return await _context.Alumnos.ToListAsync();
        }

        //CONSULTA SELECT * FROM WHERE ID = N
        // GET:api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumno(decimal id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }

            return alumno;
        }

        //A L T A S
        // POST: api/[controller]
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno alumno) //nombre del modelo
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlumno), new { Id = alumno.Id }, alumno);  //minuscula
        }


        //C A M B I O S
        //// PUT:api/[controller]/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(decimal id, Alumno alumno)  //Este metodo es para actualizar, está decorado con  [HttpPut("{id}")] 
        {
            if (id != alumno.Id)
            {
                return BadRequest();  //Si el id no es igual que lo que se pusó en xxx.ID regresa un  BadReuqest()
            }
            _context.Entry(alumno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();  //intenta salvar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent(); //si todo estuvo bien retorna un NoContect (), por eso vemos el 204.
        }

        //B A J A S 
        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Alumno>> DeleteAlumno(decimal id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlumnoExists(decimal id)
        {
            return _context.Alumnos.Any(e => e.Id == id);
        }
    }
}

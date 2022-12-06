using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API_ESCUELA.Models;

namespace WEB_API_ESCUELA.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/curso")]//----<--------ojo
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ESCUELA_PROYECTOContext _context; //se la pasan las inyecciones de ependencia
        /*A través del contexto de datos, recibe el constructor y lo almacena en esta variable privada (_context)*/
        public CursoController(ESCUELA_PROYECTOContext context)
        {
            _context = context;  //se iguala la variable local por la pública
        }

        //CONSULTA SELECT * FROM
        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCurso()
        {
            return await _context.Cursos.ToListAsync();
        }

        //CONSULTA SELECT * FROM WHERE ID = N
        // GET:api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(decimal id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        //A L T A S
        // POST: api/[controller]
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso) //nombre del modelo
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurso), new { Id = curso.Id }, curso);  //minuscula
        }


        //C A M B I O S
        //// PUT:api/[controller]/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(decimal id, Curso curso)  //Este metodo es para actualizar, está decorado con  [HttpPut("{id}")] 
        {
            if (id != curso.Id)
            {
                return BadRequest();  //Si el id no es igual que lo que se pusó en xxx.ID regresa un  BadReuqest()
            }
            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();  //intenta salvar cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent(); //si todo estuvo bien retorna un NoContect (), por eso vemos el 204.
        }

        //B A J A S 
        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Curso>> DeleteProfesor(decimal id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(decimal id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}


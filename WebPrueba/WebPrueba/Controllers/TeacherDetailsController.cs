using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPrueba.Models;

namespace WebPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherDetailsController : ControllerBase
    {
        private readonly SincoABRContext _context;

        public TeacherDetailsController(SincoABRContext context)
        {
            _context = context;
        }

        // GET: api/TeacherDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDetail>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        // GET: api/TeacherDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDetail>> GetTeacherDetail(int id)
        {
            var teacherDetail = await _context.Teachers.FindAsync(id);

            if (teacherDetail == null)
            {
                return NotFound();
            }

            return teacherDetail;
        }

        // PUT: api/TeacherDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacherDetail(int id, TeacherDetail teacherDetail)
        {
            if (id != teacherDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(teacherDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/TeacherDetails
        [HttpPost]
        public async Task<ActionResult<TeacherDetail>> PostTeacherDetail(TeacherDetail teacherDetail)
        {
            _context.Teachers.Add(teacherDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacherDetail", new { id = teacherDetail.Id }, teacherDetail);
        }

        // DELETE: api/TeacherDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TeacherDetail>> DeleteTeacherDetail(int id)
        {
            var teacherDetail = await _context.Teachers.FindAsync(id);
            if (teacherDetail == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacherDetail);
            await _context.SaveChangesAsync();

            return teacherDetail;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPrueba.Models;

namespace WebPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectsController : ControllerBase
    {
        private readonly SincoABRContext _context;

        public StudentSubjectsController(SincoABRContext context)
        {
            _context = context;
        }

        // GET: api/StudentSubjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentSubject>>> GetStudentSubject()
        {
            return await _context.StudentSubject.ToListAsync();
        }

        // GET: api/StudentSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentSubject>> GetStudentSubject(int id)
        {
            var studentSubject = await _context.StudentSubject.FindAsync(id);

            if (studentSubject == null)
            {
                return NotFound();
            }

            return studentSubject;
        }

        // PUT: api/StudentSubjects/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentSubject(int id, StudentSubject studentSubject)
        {
            if (id != studentSubject.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(studentSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentSubjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentSubjects
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<StudentSubject>> PostStudentSubject(StudentSubject studentSubject)
        {
            _context.StudentSubject.Add(studentSubject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentSubjectExists(studentSubject.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentSubject", new { id = studentSubject.StudentId }, studentSubject);
        }

        // DELETE: api/StudentSubjects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentSubject>> DeleteStudentSubject(int id)
        {
            var studentSubject = await _context.StudentSubject.FindAsync(id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            _context.StudentSubject.Remove(studentSubject);
            await _context.SaveChangesAsync();

            return studentSubject;
        }

        private bool StudentSubjectExists(int id)
        {
            return _context.StudentSubject.Any(e => e.StudentId == id);
        }
    }
}

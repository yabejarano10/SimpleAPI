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
    public class SubjectDetailsController : ControllerBase
    {
        private readonly SincoABRContext _context;

        public SubjectDetailsController(SincoABRContext context)
        {
            _context = context;
        }

        // GET: api/SubjectDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDetail>>> GetSubjects()
        {
            return await _context.Subjects.Include(t => t.Teacher).ToListAsync();
        }

        // GET: api/SubjectDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDetail>> GetSubjectDetail(int id)
        {
            var subjectDetail = await _context.Subjects.FindAsync(id);

            if (subjectDetail == null)
            {
                return NotFound();
            }

            return subjectDetail;
        }

        // PUT: api/SubjectDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjectDetail(int id, SubjectDetail subjectDetail)
        {

            if (id != subjectDetail.Id)
            {
                return BadRequest();
            }

            var item = subjectDetail;
            item.Teacher = _context.Teachers.FirstOrDefault(teacher => teacher.Id == subjectDetail.Teacher.Id);


            _context.Entry(item).State = EntityState.Modified;

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

        // POST: api/SubjectDetails
        [HttpPost]
        public async Task<ActionResult<SubjectDetail>> PostSubjectDetail(SubjectDetail subjectDetail)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == subjectDetail.Teacher.Id);
            var subject = new SubjectDetail
            {
                SubjectName = subjectDetail.SubjectName,
                Teacher = teacher
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubjectDetail", new { id = subjectDetail.Id }, subjectDetail);
        }

        // DELETE: api/SubjectDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SubjectDetail>> DeleteSubjectDetail(int id)
        {
            var subjectDetail = await _context.Subjects.FindAsync(id);
            if (subjectDetail == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subjectDetail);
            await _context.SaveChangesAsync();

            return subjectDetail;
        }
    }
}

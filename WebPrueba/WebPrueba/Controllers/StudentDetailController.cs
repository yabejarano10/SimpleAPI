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
    public class StudentDetailController : ControllerBase
    {
        private readonly SincoABRContext _context;

        public StudentDetailController(SincoABRContext context)
        {
            _context = context;
        }

        // GET: api/StudentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDetail>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/StudentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentSubjectDetail>> GetStudentDetail(int id)
        {
            var student = _context.Students.Include(ss => ss.StudentSubjects).FirstOrDefault(stu => stu.Id == id);
            var subjectsExcluding = new List<SubjectDetail>();

            var subjects = await _context.StudentSubject
                           .Include(s => s.Subject)
                           .Where(sid => sid.StudentId == student.Id)
                           .Select(st => st.Subject)
                           .ToListAsync();


            foreach (var subject in subjects)
            {
                var subject2 = new SubjectDetail
                {
                    Id = subject.Id,
                    SubjectName = subject.SubjectName
                };
                subjectsExcluding.Add(subject2);
                
            }
            var fullStudent = new StudentSubjectDetail
            {

                Student = new StudentDetail
                {
                    Id = student.Id,
                    Name = student.Name
                },
                Subjects = subjectsExcluding
            };


            return fullStudent;
        }

        [HttpGet("report")]
        public async Task<ICollection<ReportDetail>> GetStudentsReport()
        {
            ICollection<ReportDetail> report = new List<ReportDetail>();
            var students = _context.Students.Include(ss => ss.StudentSubjects);
            

            foreach(var student in students)
            {
                var subjects = await _context.StudentSubject
                               .Include(s => s.Subject)
                               .Where(sid => sid.StudentId == student.Id)
                               .Select(st => st.Subject)
                               .ToListAsync();

                foreach(var subject in subjects)
                {
                    var subjectWithTeacher = _context.Subjects.Include(t => t.Teacher).FirstOrDefault(s => s.Id == subject.Id);
                    var grade = _context.StudentSubject.Where(s => s.SubjectId == subject.Id).Where(s => s.StudentId == student.Id).FirstOrDefault();

                    var reportElement = new ReportDetail
                    {

                        Student = new StudentDetail
                        {
                            Id = student.Id,
                            Name = student.Name
                        },
                        Subject = new SubjectDetail
                        {
                            Id = subjectWithTeacher.Id,
                            SubjectName = subjectWithTeacher.SubjectName,
                            Teacher = subjectWithTeacher.Teacher
                        },
                        Grade = grade.Grade


                    };
                    report.Add(reportElement);
                }

            }
            return report; 
        }



        // PUT: api/StudentDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentDetail(int id, StudentDetail studentDetail)
        {
            if (id != studentDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentDetail).State = EntityState.Modified;

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

        // POST: api/StudentDetail
        [HttpPost]
        public async Task<ActionResult<StudentDetail>> PostStudentDetail(StudentDetail studentDetail)
        {

            _context.Students.Add(studentDetail);
            _context.SaveChanges();
            StudentSubjectsController stSub = new StudentSubjectsController(_context);

            foreach (var item in studentDetail.StudentSubjects)
            {
                var subject = _context.Subjects.FirstOrDefault(sub => sub.Id == item.SubjectId);
                var relation = new StudentSubject
                {
                    StudentId = studentDetail.Id,
                    SubjectId = subject.Id,
                    Grade = "0.0"
                };
                stSub.PostStudentSubject(relation);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentDetail", new { id = studentDetail.Id }, studentDetail);
        }

        // DELETE: api/StudentDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentDetail>> DeleteStudentDetail(int id)
        {
            var studentDetail = await _context.Students.FindAsync(id);
            if (studentDetail == null)
            {
                return NotFound();
            }

            _context.Students.Remove(studentDetail);
            await _context.SaveChangesAsync();

            return studentDetail;
        }
    }
}

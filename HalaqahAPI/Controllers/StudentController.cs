using HalaqahModel.Helpers;
using Microsoft.AspNetCore.Mvc;
using HalaqahModel.Models;
using HalaqahAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace HalaqahAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(StudentService service, EntityHelper entityHelper) : ControllerBase
    {
        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            return Ok(service.GetAllStudents()
                .Include(s => s.Person)
                .Select(st => entityHelper.OnlyInclude(st, nameof(Student.Person)))
                .AsEnumerable());
        }
        
        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = service.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            return entityHelper.AlsoInclude(student, nameof(Student.Person));
        }

        // POST: api/Student/{id}/markAttendance
        [HttpPost("{id}/markAttendance")]
        public async Task<IActionResult> MarkAttendance(int id, StudentAttendance attendanceRecord)
        {
            try
            {
                service.MarkAttendance(id, attendanceRecord);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            return NoContent();
        }
    }
}

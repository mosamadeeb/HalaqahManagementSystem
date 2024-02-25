using HalaqahModel.Helpers;
using Microsoft.AspNetCore.Mvc;
using HalaqahModel.Models;
using HalaqahAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace HalaqahAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController(StudentService service, EntityHelper entityHelper) : ControllerBase
    {
        // GET: api/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            return Ok(service.GetAllStudents()
                .Select(st => entityHelper.OnlyInclude(st))
                .AsEnumerable());
        }
        
        // GET: api/student/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = service.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // POST: api/student/attendance
        [HttpPost("attendance")]
        public async Task<IActionResult> MarkAttendance(IEnumerable<StudentAttendance> attendanceRecords)
        {
            try
            {
                foreach (var record in attendanceRecords)
                {
                    record.Timestamp = DateTime.Today;
                    service.MarkAttendance(record);
                }
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            return Ok();
        }
    }
}

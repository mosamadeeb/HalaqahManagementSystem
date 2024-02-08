using HalaqahModel.Helpers;
using Microsoft.AspNetCore.Mvc;
using HalaqahModel.Models;
using HalaqahAPI.Services;

namespace HalaqahAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(StudentService service, EntityHelper entityHelper) : ControllerBase
    {
        [HttpGet]
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

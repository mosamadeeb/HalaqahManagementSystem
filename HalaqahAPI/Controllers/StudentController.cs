using Microsoft.AspNetCore.Mvc;
using HalaqahAPI.Models;
using HalaqahAPI.Services;

namespace HalaqahAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(StudentService service) : ControllerBase
    {
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

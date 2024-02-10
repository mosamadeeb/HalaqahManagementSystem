using HalaqahModel.Helpers;
using Microsoft.AspNetCore.Mvc;
using HalaqahModel.Models;
using HalaqahAPI.Services;

namespace HalaqahAPI.Controllers
{
    [Route("api/halaqah")]
    [ApiController]
    public class HalaqahController(HalaqahService service, EntityHelper entityHelper) : ControllerBase
    {
        // GET: api/halaqah/{id}/students
        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsInHalaqah(int id)
        {
            return Ok(service.GetStudentsInHalaqah(id)
                .Select(st => entityHelper.OnlyInclude(st, nameof(Student.Person)))
                .AsEnumerable());
        }
    }
}

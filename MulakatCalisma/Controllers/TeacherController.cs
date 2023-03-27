using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;
using NUnit.Framework;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Teacher>>> CreateTeacher(Teacher teacher)
        {
            var result = await _teacherService.CreateTeacher(teacher);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<Teacher>>>> GetTeacherCourse()
        {
            var result = await _teacherService.GetTeachers();
            return Ok(result);
        }
    }
}

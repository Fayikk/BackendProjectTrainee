using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Context;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;

namespace MulakatCalisma.Services.Concrete
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        public TeacherService(ApplicationDbContext context,IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Teacher>> CreateTeacher(Teacher teacher)
        {
            var user = _authService.GetUserId();
            teacher.Userıd = user;
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Teacher>
            {
                Data = teacher,
                Message = "Success",
                Success = true,

            };
        }

        public async Task<ServiceResponse<List<Teacher>>> GetTeachers()
        {
            var user = _authService.GetUserId();
            var result = await _context.Teachers.Where(x => x.Userıd == user).ToListAsync();
            if (result == null)
            {
                return new ServiceResponse<List<Teacher>>
                {
                    Success = false,
                    Message = "Teachers courses not found",
                };
            }
            return new ServiceResponse<List<Teacher>>
            {
                Data = result,
                Message = "Success",
                Success = true,
            };
        }
    }
}

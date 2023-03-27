using MulakatCalisma.Entity;

namespace MulakatCalisma.Services.Abstract
{
    public interface ITeacherService
    {
        Task<ServiceResponse<Teacher>> CreateTeacher(Teacher teacher);
        Task<ServiceResponse<List<Teacher>>> GetTeachers();
    }
}

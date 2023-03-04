using MulakatCalisma.Entity;

namespace MulakatCalisma.Services.Abstract
{
    public interface IStarService
    {
        Task<ServiceResponse<bool>> GiveStar(Star star);

    }
}

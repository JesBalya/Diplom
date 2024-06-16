using Diplom.Models.Entity;
using Diplom.ViewModels;

namespace Diplom.Services.Interfaces
{
    public interface IConsultationService
    {
        Task<IBaseResponse<bool>> AddCosultation(ConsultationViewModel model);
        Task<IBaseResponse<bool>> Sub(int consId, string userName);
        Task<IBaseResponse<bool>> DeleteConsultation(int consId);
    }
}

using Diplom.Models.Entity;
using Diplom.ViewModels;

namespace Diplom.Services.Interfaces
{
    public interface IConsultationService
    {
        Task<IBaseResponse<bool>> AddCosultation(ConsultationViewModel model);
        Task<IBaseResponse<bool>> Sub(int consId, string userName);
        Task<IBaseResponse<bool>> DeleteConsultation(int consId);
        Task<IBaseResponse<List<Consultation>>> GetMyCons(string userName);
        Task<IBaseResponse<ConsultationViewModel>> GetCons(int id);
        Task<IBaseResponse<bool>> UpdateCons(ConsultationViewModel model);
        Task<IBaseResponse<bool>> Unsub(int consId, string userName);
        Task<IBaseResponse<bool>> IsSub(int consId, string userName);
    }
}

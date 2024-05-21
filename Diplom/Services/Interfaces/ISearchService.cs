using Diplom.Models.Entity;
using Diplom.Services.Implementations;

namespace Diplom.Services.Interfaces
{
    public interface ISearchService
    {
        Task<BaseResponse<IEnumerable<Consultation>>> getConsultations(string Name);
    }
}

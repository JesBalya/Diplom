using Diplom.Models.Account;
using Diplom.Models.Entity;
using Diplom.Services.Implementations.Repositories;
using Diplom.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Diplom.Services.Implementations
{
    public class SearchService: ISearchService
    {
        private readonly IBaseRepository<Subscription> _subscriptionRepository;
        private readonly IBaseRepository<Consultation> _consultationRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public SearchService(IBaseRepository<Subscription> sr, IBaseRepository<User> ur,IBaseRepository<Consultation> cr,ILogger<AccountService> logger)
        {
            _subscriptionRepository = sr;
            _userRepository = ur;
            _consultationRepository = cr;
            _logger = logger;
        }

        public async Task<BaseResponse<IEnumerable<Consultation>>> getConsultations(string Name)
        {
            try
            {
                var respronse = await _userRepository.GetAll()
                    .Include(x => x.Subscription)
                    .ThenInclude(x => x.Consultations)
                    .FirstOrDefaultAsync(x => x.Name == Name);
                

                return new BaseResponse<IEnumerable<Consultation>>()
                {
                    Data = respronse.Subscription.Consultations,
                    StatusCode = StatusCode.OK,
                    Description = "Консультации загрузились",
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<IEnumerable<Consultation>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}

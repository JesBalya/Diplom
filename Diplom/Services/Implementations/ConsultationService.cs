using Diplom.Models.Account;
using Diplom.Models.Entity;
using Diplom.Services.Implementations.Repositories;
using Diplom.Services.Interfaces;
using Diplom.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Services.Implementations
{
    public class ConsultationService : IConsultationService
    {
        private readonly IBaseRepository<Consultation> _consultationRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Subscription> _subRepository;
        private readonly ILogger<AccountService> _logger;

        public ConsultationService(IBaseRepository<User> ur, IBaseRepository<Consultation> cr,IBaseRepository<Subscription> sr, ILogger<AccountService> logger)
        {
            _userRepository = ur;
            _subRepository = sr;
            _consultationRepository = cr;
            _logger = logger;
        }
        public async Task<IBaseResponse<bool>> AddCosultation(ConsultationViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.UserName);

                var consultation = new Consultation()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Date = model.Date,
                    User = user,
                    UserId = user.Id,
                };
                user.MyConsultations.Add(consultation);

                await _consultationRepository.Create(consultation);
                await _userRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Объект добавился"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[AddConsultation]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteConsultation(int consId)
        {
            try
            {
                var cons = await _consultationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == consId);
                await _consultationRepository.Delete(cons);
                
                return new BaseResponse<bool>()
                {
                    Data= true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Sub(int consId, string userName)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);
                var consultation = await _consultationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == consId);

                var sub = await _subRepository.GetAll()
                    .Include(x => x.Consultations)
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                sub.Consultations.Add(consultation);
                await _subRepository.Update(sub);

                return new BaseResponse<bool>() 
                { 
                    Data = true,
                    StatusCode = StatusCode.OK ,
                    Description = "Всё чётка"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<Consultation>>> GetMyCons(string userName)
        {
            try
            {
                var user = _userRepository.GetAll().Include(x => x.MyConsultations).FirstOrDefault(x => x.Name == userName);

                return new BaseResponse<List<Consultation>>()
                {
                    Data = user.MyConsultations.ToList(),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Consultation>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }

        public async Task<IBaseResponse<ConsultationViewModel>> GetCons(int id)
        {
            try
            {
                var cons = await _consultationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                var response = new ConsultationViewModel()
                {
                    Id = cons.Id,
                    Date = cons.Date,
                    Description = cons.Description,
                    Name = cons.Name,
                };

                return new BaseResponse<ConsultationViewModel>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ConsultationViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }

        public async Task<IBaseResponse<bool>> UpdateCons(ConsultationViewModel consultationToUpdate)
        {
            try
            {
                var consultation = await _consultationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == consultationToUpdate.Id);
                if(consultation != null)
                {
                    consultation.Description = consultationToUpdate.Description;
                    consultation.Date = consultationToUpdate.Date;
                    consultation.Name = consultationToUpdate.Name;
                }
                _consultationRepository.Update(consultation);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}

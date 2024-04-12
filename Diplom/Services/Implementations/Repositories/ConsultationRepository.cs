using Diplom.Models.Entity;
using Diplom.Services.Interfaces;
using Diplom.AppDbContext;

namespace Diplom.Services.Implementations.Repositories
{
    public class ConsultationRepository: IBaseRepository<Consultation>
    {
        private ApplicationDbContext _context;
       
        public ConsultationRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task Create(Consultation entity)
        {
            await _context.Consultations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Consultation entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Consultation> GetAll()
        {
            return _context.Consultations;
        }

        public async Task<Consultation> Update(Consultation entity)
        {
            _context.Consultations.Update(entity);
            await _context.SaveChangesAsync();  
            return entity;
        }
    }
}

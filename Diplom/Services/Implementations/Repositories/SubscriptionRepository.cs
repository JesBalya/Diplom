using Diplom.Models.Entity;
using Diplom.Services.Interfaces;
using Diplom.AppDbContext;

namespace Diplom.Services.Implementations.Repositories
{
    public class SubscriptionRepository : IBaseRepository<Subscription>
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Subscription entity)
        {
            await _context.Subscriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Subscription entity)
        {
           _context.Subscriptions.Remove(entity);
           await _context.SaveChangesAsync();
        }

        public IQueryable<Subscription> GetAll()
        {
           return _context.Subscriptions;
        }

        public async Task<Subscription> Update(Subscription entity)
        {
            _context.Subscriptions.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

using Diplom.AppDbContext;
using Diplom.Models.Account;
using Diplom.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Services.Implementations.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public async Task Delete(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Create(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Update(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<int> GetIdByString(string Name)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Name == Name);
            return result.Id;
        }

    }
}

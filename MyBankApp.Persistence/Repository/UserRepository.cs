using Microsoft.EntityFrameworkCore;
using MyBankApp.Application.Contracts.Persistence;
using MyBankApp.Domain.Entities;
using MyBankApp.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Persistence.Repository
{
    public class UserRepository : AsyncRepository<User>, IUserRepository
    {
        private readonly MyBankAppDbContext _dbContext;
        public UserRepository(MyBankAppDbContext dbContext) : base(dbContext)
        {

        }
        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Set<User>().ToListAsync();
        }
        public override async Task UpdateAsync(User entity)
        {
            var existingUser = await _dbContext.Set<User>().Where(a => a.Id == entity.Id).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                _dbContext.Set<User>().Update(existingUser);
            }
        }
        public override async Task DeleteAsync(User entity)
        {
            var existingUser = await _dbContext.Set<User>().Where(a => a.Id == entity.Id).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                _dbContext.Set<User>().Remove(existingUser);
            }
        }

        public Task<bool> isUniqueUser(string username, string Email)
        {
            throw new NotImplementedException();
        }
    }
}

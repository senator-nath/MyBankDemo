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
    public class StateRepository : AsyncRepository<State>, IStateRepository
    {
        private readonly MyBankAppDbContext _dbContext;
        public StateRepository(MyBankAppDbContext dbContext) : base(dbContext)
        {

        }
        public override async Task<IEnumerable<State>> GetAllAsync()
        {
            return await _dbContext.Set<State>().ToListAsync();
        }
    }
}

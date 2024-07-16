using MyBankApp.Application.Configuration;
using MyBankApp.Application.Contracts.Persistence;
using MyBankApp.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MyBankAppDbContext _dbContext;

        public IUserRepository user { get; private set; }

        public IStateRepository state { get; private set; }

        public ILGARepository lgaRepository { get; private set; }

        public IAccountLimitRepository accountLimit { get; private set; }

        public IGenderRepository gender { get; private set; }

        public UnitOfWork(MyBankAppDbContext dbContext)
        {
            _dbContext = dbContext;

            user = new UserRepository(_dbContext);
        }
        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.DisposeAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MyBankApp.Application.Contracts.Persistence;
using MyBankApp.Domain.Dto.ResponseDto;
using MyBankApp.Domain.Entities;
using MyBankApp.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Persistence.Repository
{
    public class LGARepository : AsyncRepository<LGA>, ILGARepository
    {
        private readonly MyBankAppDbContext _dbContext;
        public LGARepository(MyBankAppDbContext dbContext) : base(dbContext)
        {

        }

        public override async Task<IEnumerable<LGA>> GetAllAsync()
        {
            return await _dbContext.Set<LGA>().ToListAsync();
        }
        public async Task<IEnumerable<LGAResponseDto>> GetByStateIdAsync(int stateId)
        {
            return await _dbContext.Set<LGAResponseDto>()
                .Where(lga => lga.StateId == stateId)
                .ToListAsync();
        }




    }
}

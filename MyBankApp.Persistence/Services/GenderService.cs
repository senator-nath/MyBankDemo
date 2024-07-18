using MyBankApp.Application.Configuration;
using MyBankApp.Application.Contracts.IServices;
using MyBankApp.Application.Contracts.Persistence;
using MyBankApp.Domain.Dto.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyBankApp.Persistence.Services
{
    public class GenderService : IGenderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<GenderRequestDto>> GetAllPostsAsync()
        {
            if (_unitOfWork != null)
            {
                var genders = await _unitOfWork.gender.GetAllAsync();

                return genders.Select(g => new GenderRequestDto
                {
                    Id = g.Id,
                    Description = g.Description
                });
            }
            return Enumerable.Empty<GenderRequestDto>();


        }

    }
}

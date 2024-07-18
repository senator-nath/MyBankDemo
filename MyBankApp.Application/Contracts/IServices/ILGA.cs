using MyBankApp.Domain.Dto.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Application.Contracts.IServices
{
    public interface ILGA
    {
        Task<List<GenderRequestDto>> GetAllPostsAsync();
    }
}

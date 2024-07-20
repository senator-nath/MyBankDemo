﻿using MyBankApp.Domain.Dto.RequestDto;
using MyBankApp.Domain.Dto.ResponseDto;
using MyBankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<bool> isUniqueUser(UserRequestDto entity);
    }
}

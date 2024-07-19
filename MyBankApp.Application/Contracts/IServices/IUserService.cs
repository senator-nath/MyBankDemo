using MyBankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Application.Contracts.IServices
{
    public interface IUserService
    {
        Task<bool> IsUniqueUser(User entity);

        Task<User> Register(User entity);
    }
}

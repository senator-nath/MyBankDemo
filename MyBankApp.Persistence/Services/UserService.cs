using MyBankApp.Application.Configuration;
using MyBankApp.Application.Contracts.IServices;
using MyBankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Persistence.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsUniqueUser(User entity)
        {
            var existingUser = await _unitOfWork.user.isUniqueUser(entity);
            return existingUser;

        }

        public async Task<User> Register(User entity)
        {
            var existingUser = await IsUniqueUser(entity);
            if (!existingUser)
            {
                throw new InvalidOperationException("User already exists");
            }

            await _unitOfWork.user.CreateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity;
        }
    }
}

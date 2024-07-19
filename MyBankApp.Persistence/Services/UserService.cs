using Microsoft.Extensions.Logging;
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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> IsUniqueUser(User entity)
        {
            try
            {
                var existingUser = await _unitOfWork.user.isUniqueUser(entity);
                return existingUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if user is unique");
                throw;
            }
        }

        public async Task<User> Register(User entity)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
                throw;
            }
        }
    }
}

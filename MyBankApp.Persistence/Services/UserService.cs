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

        //public async Task<bool> IsUniqueUser(User entity)
        //{
        //    var existingUser = await _unitOfWork.user.isUniqueUser(x => x.UserName == entity.UserName || x.Email == entity.Email);
        //    return existingUser == null;
        //}

        //public async Task<User> Register(User entity)
        //{
        //    if (!await IsUniqueUser(entity))
        //    {
        //        throw new InvalidOperationException("User already exists");
        //    }

        //    _unitOfWork.user.CreateAsync(entity);
        //    await _unitOfWork.CompleteAsync();
        //    return entity;
        //}
    }
}

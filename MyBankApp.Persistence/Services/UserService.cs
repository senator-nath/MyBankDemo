using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyBankApp.Application.Configuration;
using MyBankApp.Application.Contracts.IServices;
using MyBankApp.Application.validator;
using MyBankApp.Domain.Dto.RequestDto;
using MyBankApp.Domain.Dto.ResponseDto;
using MyBankApp.Domain.Entities;
using MyBankApp.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyBankApp.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;
        private readonly UserRequestValidator _validator;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IConfiguration configuration, UserRequestValidator validator, AppSettings appSettings)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
            _validator = validator;
            _appSettings = appSettings;
        }

        public async Task<bool> IsUniqueUser(UserRequestDto entity)
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
        public async Task<UserResponseDetails> Register(UserRequestDto entity)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(entity);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
                var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, entity.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

                var token = GetToken(authClaims);

                var finalToken = new JwtSecurityTokenHandler().WriteToken(token);
                var user_exist = await _unitOfWork.user.isUniqueUser(entity);

                if (user_exist)
                {
                    _logger.LogError("User already exists");
                    return new UserResponseDetails()
                    {
                        Message = $"User with the email {entity.Email} already exists. Please login",
                        IsSuccess = false
                    };
                }

                var user = new User()
                {
                    Email = entity.Email,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    Address = entity.Address,
                    LastName = entity.LastName,
                    PhoneNumber = entity.PhoneNumber,
                    Dob = entity.Dob,
                    Gender = entity.Gender,
                    StateId = entity.StateId,
                    LGAId = entity.LGAId,
                    Age = CalculateAgeFromDateOfBirth(entity.Dob),
                    UserName = entity.UserName,
                    Title = entity.Title,
                    accountType = entity.AccountType,
                    LandMark = entity.LandMark,
                    NIN = entity.NIN,
                    HasBvn = entity.HasBvn,
                    Bvn = entity.Bvn,
                    HashPassword = Helper.Helper.HashPassword(entity.Password),
                    AccountNo = Generate11DigitRandomNumber(),
                };

                await _unitOfWork.user.CreateAsync(user);
                await _unitOfWork.CompleteAsync();

                var response = new UserResponseDto()
                {
                    LastLogin = "Now",
                    Token = finalToken,
                    DailyLimitBalance = "",
                    AccountNumber = user.AccountNo,
                    UserName = user.UserName,
                    AccountName = user.FirstName + " " + user.MiddleName + " " + user.LastName,
                    Title = user.Title,
                    GenderId = user.GenderId,
                    AccountType = user.accountType,
                    Bvn = user.Bvn,
                    NIN = user.NIN,
                    Status = "",
                };

                return new UserResponseDetails()
                {
                    Message = "",
                    IsSuccess = true,
                    ResponseDetails = response,
                    Token = finalToken,

                };
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error");
                return new UserResponseDetails()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
                return new UserResponseDetails()
                {
                    Message = "Server error",
                    IsSuccess = false
                };
            }
        }



        private string CalculateAgeFromDateOfBirth(DateTime Dob)
        {
            var today = DateTime.Today;
            var age = today.Year - Dob.Year;

            if (Dob.Date > today.AddYears(-age)) age--;


            var Age = age.ToString();
            return Age;
        }

        private static string Generate11DigitRandomNumber()
        {
            Random random = new Random();
            string result = string.Empty;

            // Generate the first 6 digits
            result += random.Next(100000, 1000000).ToString("D6");

            // Generate the remaining 5 digits
            result += random.Next(10000, 100000).ToString("D5");

            return result;
        }
        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature));

            return token;
        }
        //private string GenerateToken(User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("your-secret-key");
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //    new Claim(ClaimTypes.Name, user.UserName),
        //    new Claim(ClaimTypes.Email, user.Email),
        //    new Claim(ClaimTypes.Role, user.accountType.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(30),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

    }
}





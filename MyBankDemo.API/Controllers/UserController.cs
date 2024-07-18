using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBankApp.Application.Configuration;
using MyBankApp.Application.validator;
using MyBankApp.Domain.Dto.RequestDto;
using MyBankApp.Domain.Entities;
using System.Threading.Tasks;

namespace MyBankDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IUnitOfWork _unitOfWork;

        //public UserController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        //[HttpPost]
        //public async Task<UserRequestDto> CreateUser(UserRequestDto user)
        //{

        //}
        //[HttpPut]
        //public async Task<UserRequestDto> UpdateUser(UserRequestDto user)
    }
}

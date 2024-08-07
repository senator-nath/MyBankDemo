﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBankApp.Application.Configuration;
using MyBankApp.Application.Contracts.IServices;
using MyBankApp.Application.validator;
using MyBankApp.Domain.Dto.RequestDto;
using MyBankApp.Domain.Entities;
using MyBankApp.Persistence.Services;
using Serilog;
using System;
using System.Threading.Tasks;

namespace MyBankDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] UserRequestDto user)
        {
            try
            {
                var result = await _userService.Register(user);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyFirstCore.Data;
using MyFirstCore.Dtos;
using MyFirstCore.Models;

namespace MyFirstCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _repo;

        public IConfiguration _Config { get; }

        public AuthController(IAuthRepository repo,IConfiguration config )
        {
            _repo = repo;
            _Config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult>Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _repo.UserExsits(userForRegisterDto.Username))
                return BadRequest("User already exists");
            var userToCreate = new User {
                UserName = userForRegisterDto.Username
            };
            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);
           return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult>Login(UserForLoginDto userForLoginDto)
        {
            var USerfromRepo =await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);
            if (USerfromRepo == null)
                return Unauthorized();

            var Claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier,USerfromRepo.USerId.ToString()),
              new Claim(ClaimTypes.Name,USerfromRepo.UserName)
            };
            var Key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config.GetSection("AppSettings:Token").Value));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials=Creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

    }
}
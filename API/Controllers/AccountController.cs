using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks; // ตรวจสอบว่ามีการรวมเนมสเปซนี้สำหรับ Task

using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext dataContext, ITokenService tokenService)
        {
            _dataContext = dataContext;
            _tokenService = tokenService;
        }

        private async Task<bool> IsUserExists(string username)
        {
            return await _dataContext.Users.AnyAsync(user => user.UserName == username.ToLower());
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await IsUserExists(registerDto.Username!))
            {
                return BadRequest("ชื่อผู้ใช้นี้มีอยู่แล้ว");
            }

            using var hmacSHA256 = new HMACSHA256();
            var user = new AppUser
            {
                UserName = registerDto.Username!.Trim().ToLower(),
                PasswordHash = hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password!.Trim())),
                PasswordSalt = hmacSHA256.Key
            };

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.UserName);

            if (user is null)
            {
                return Unauthorized("ชื่อผู้ใช้ไม่ถูกต้อง");
            }

            using var hmacSHA256 = new HMACSHA256(user.PasswordSalt!);
            var computedHash = hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password!.Trim()));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash?[i])
                {
                    return Unauthorized("รหัสผ่านไม่ถูกต้อง");
                }
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}

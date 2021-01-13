using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serendipity.Data;
using Serendipity.DTOs;
using Serendipity.Entities;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Serendipity.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerdto)
        {
            if (await UserExists(registerdto.Username))
                return BadRequest("Username taken");

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerdto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerdto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync( x=> x.UserName == username.ToLower());
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serendipity.Data;
using Serendipity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendipity.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;
        }

        // api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        //api/users/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task< ActionResult<AppUser>> GetUser(int id)
        {
            return await context.Users.FindAsync(id); 
        }
    }
}

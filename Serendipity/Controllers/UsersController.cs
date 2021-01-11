using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serendipity.Data;
using Serendipity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendipity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task< ActionResult<AppUser>> GetUser(int id)
        {
            return await context.Users.FindAsync(id); 
        }
    }
}

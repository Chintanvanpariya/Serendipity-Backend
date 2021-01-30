using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serendipity.DTOs;
using Serendipity.Entities;
using Serendipity.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendipity.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public UsersController(IUserRepository userRepo, IMapper mapper, IPhotoService photoService)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
            this.photoService = photoService;
        }

        // api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await userRepo.GetMembersAsync();

            return Ok(users);
        }

        //api/users/1
        [HttpGet("{username}", Name ="GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await userRepo.GetMemberAsync(username);


        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto  memberupdatedto )        
        {
            var user = await userRepo.GetUserByUsernameAsync(User.GetUsername());

            mapper.Map(memberupdatedto, user);

            userRepo.Update(user);
            if (await userRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }


        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await userRepo.GetUserByUsernameAsync(User.GetUsername());

            var result = await photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            if(user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if(await userRepo.SaveAllAsync())
            {
                return CreatedAtRoute("GetUser", new { USername = user.UserName },mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Problem occurred ");
        }

    }
}

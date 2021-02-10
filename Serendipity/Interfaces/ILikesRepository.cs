using Serendipity.DTOs;
using Serendipity.Entities;
using Serendipity.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendipity.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}

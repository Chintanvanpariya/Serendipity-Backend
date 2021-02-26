using System.Collections.Generic;
using System.Threading.Tasks;
using Serendipity.DTOs;
using Serendipity.Entities;
using Serendipity.Helpers;

namespace Serendipity.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}
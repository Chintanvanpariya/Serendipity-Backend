using System.Threading.Tasks;
using Serendipity.Entities;

namespace Serendipity.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
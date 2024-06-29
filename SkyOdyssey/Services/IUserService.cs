using SkyOdyssey.DTOs;
using System.Threading.Tasks;

namespace SkyOdyssey.Services
{
    public interface IUserService
    {
        Task<UserDto> Authenticate(string username, string password);
        Task<UserDto> Register(RegisterUserDto registerUserDto);
    }
}

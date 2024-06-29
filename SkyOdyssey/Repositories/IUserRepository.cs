using SkyOdyssey.Models;
using System.Threading.Tasks;

namespace SkyOdyssey.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
}

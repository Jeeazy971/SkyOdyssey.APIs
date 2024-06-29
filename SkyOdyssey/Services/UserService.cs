using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using SkyOdyssey.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkyOdyssey.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Register(RegisterUserDto registerUserDto)
        {
            if (await _userRepository.GetByUsernameAsync(registerUserDto.Username) != null)
                return null;

            CreatePasswordHash(registerUserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = registerUserDto.Username,
                Email = registerUserDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _userRepository.AddAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}

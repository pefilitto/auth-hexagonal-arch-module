using auth_hexagonal_arch_module.Domain.Entities;
using auth_hexagonal_arch_module.Domain.Repository;
using auth_hexagonal_arch_module.Domain.Services;

namespace auth_hexagonal_arch_module.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userPostgresRepository;

        public UserService(IUserRepository userPostgresRepository)
        {
            _userPostgresRepository = userPostgresRepository;
        }

        public Task<List<User>> Get()
        {
            return _userPostgresRepository.Get();
        }

        public Task<User> GetById(int id)
        {
            return _userPostgresRepository.GetById(id);
        }

        public Task Save(User user)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            return _userPostgresRepository.Save(user);
        }
    }
}

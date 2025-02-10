using auth_hexagonal_arch_module.Domain.Interfaces;
using auth_hexagonal_arch_module.Infrastructure.Interfaces;

namespace auth_hexagonal_arch_module.Infrastructure.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserPostgresRepository _userRepository;
        public Task<List<Application.Entities.User>> GetAsync()
        {
            return _userRepository.GetAsync();
        }

        public Task<Application.Entities.User> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<Application.Entities.User> SaveAsync(Application.Entities.User user)
        {
            return _userRepository.SaveAsync(user);
        }
    }
}

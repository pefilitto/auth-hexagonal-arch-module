using auth_hexagonal_arch_module.Domain.Entities;

namespace auth_hexagonal_arch_module.Domain.Services
{
    public interface IUserService
    {
        public Task Save(User user);
        public Task<List<User>> Get();
        public Task<User> GetById(int id);
    }
}

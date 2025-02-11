using auth_hexagonal_arch_module.Domain.Entities;

namespace auth_hexagonal_arch_module.Domain.Services
{
    public interface IAuthService
    {
        public Task<Auth> Login(User user);
    }
}

using auth_hexagonal_arch_module.Application.Entities;

namespace auth_hexagonal_arch_module.Domain.Interfaces;

public interface IUserRepository
{
    public Task<User> SaveAsync(User user);
    public Task<List<User>> GetAsync();
    public Task<User> GetByIdAsync(int id);
}
using auth_hexagonal_arch_module.Domain.Entities;

namespace auth_hexagonal_arch_module.Domain.Repository;

public interface IUserRepository
{
    public Task<User> Save(User user);
    public Task<List<User>> Get();
    public Task<User> GetById(int id);
}
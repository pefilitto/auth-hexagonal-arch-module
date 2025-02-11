using Npgsql;
using Dapper;
using auth_hexagonal_arch_module.Domain.Entities;
using auth_hexagonal_arch_module.Domain.Repository;
namespace auth_hexagonal_arch_module.Infrastructure.Repository
{
    public class UserPostgresRepository : IUserRepository
    {
        private readonly ApplicationPostgresContext _context;

        public UserPostgresRepository(ApplicationPostgresContext context)
        {
            _context = context;
        }

        public async Task<User> Save(User user)
        {
            using var connection = new NpgsqlConnection(_context.GetConnectionString());

            string sql = "INSERT INTO users (user_name, user_cpf) VALUES (@Name, @Cpf) RETURNING id";

            var id = await connection.ExecuteScalarAsync<int>(sql, new { user.Name, user.Cpf });
            user.Id = id;
            return user;
        }

        public async Task<List<User>> Get()
        {
            using var connection = new NpgsqlConnection(_context.GetConnectionString());

            string sql = "SELECT id, user_name AS name, user_cpf AS cpf FROM users";

            var users = await connection.QueryAsync<User>(sql);
            return users.ToList();
        }

        public async Task<User> GetById(int id)
        {
            using var connection = new NpgsqlConnection(_context.GetConnectionString());

            string sql = "SELECT id, user_name AS name, user_cpf AS cpf FROM users WHERE id = @Id";

            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }
    }
}

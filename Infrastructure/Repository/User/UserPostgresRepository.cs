using auth_hexagonal_arch_module.Infrastructure.Interfaces;
using Npgsql;
using Dapper;
namespace auth_hexagonal_arch_module.Infrastructure.Repository.User
{
    public class UserPostgresRepository : IUserPostgresRepository
    {
        private readonly ApplicationPostgresContext _context;

        public UserPostgresRepository(ApplicationPostgresContext context)
        {
            _context = context;
        }

        public async Task<Application.Entities.User> SaveAsync(Application.Entities.User user)
        {
            using var connection = new NpgsqlConnection(_context.ToString());

            string sql = "INSERT INTO users (user_name, user_cpf) VALUES (@Name, @Cpf) RETURNING id";

            var id = await connection.ExecuteScalarAsync<int>(sql, new { user.Name, user.Cpf });
            user.Id = id;
            return user;
        }

        public async Task<List<Application.Entities.User>> GetAsync()
        {
            using var connection = new NpgsqlConnection(_context.ToString());

            string sql = "SELECT id, user_name AS name, user_cpf AS cpf FROM users";

            var users = await connection.QueryAsync<Application.Entities.User>(sql);
            return users.ToList();
        }

        public async Task<Application.Entities.User> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_context.ToString());

            string sql = "SELECT id, user_name AS name, user_cpf AS cpf FROM users WHERE id = @Id";

            return await connection.QueryFirstOrDefaultAsync<Application.Entities.User>(sql, new { Id = id });
        }
    }
}

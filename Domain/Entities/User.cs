namespace auth_hexagonal_arch_module.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

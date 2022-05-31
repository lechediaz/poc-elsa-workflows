namespace FactoryApp.Dtos
{
    public class UsersListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? SupervisorId { get; set; }
        public virtual UsersListDto Supervisor { get; set; }
    }
}
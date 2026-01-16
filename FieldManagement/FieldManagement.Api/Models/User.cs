namespace FieldManagement.Api.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}

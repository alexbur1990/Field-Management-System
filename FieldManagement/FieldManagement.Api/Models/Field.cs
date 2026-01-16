namespace FieldManagement.Api.Models
{
    public class Field
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public double Area { get; set; }

        public Guid OwnerUserId { get; set; }
    }
}

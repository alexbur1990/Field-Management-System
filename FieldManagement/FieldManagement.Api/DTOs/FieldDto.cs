namespace FieldManagement.Api.DTOs
{
    public class FieldDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public double Area { get; set; }
    }
}

namespace FieldManagement.Api.Models
{
    public class ControllerDevice
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;

        public Guid FieldId { get; set; }
        public Field? Field { get; set; }

        public Guid OwnerUserId { get; set; }
        public User? OwnerUser { get; set; }
    }
}

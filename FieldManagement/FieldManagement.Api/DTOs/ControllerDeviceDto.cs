namespace FieldManagement.Api.DTOs
{
    public class ControllerDeviceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public Guid FieldId { get; set; }
    }
}

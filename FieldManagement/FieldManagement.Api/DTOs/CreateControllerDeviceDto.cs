namespace FieldManagement.Api.DTOs
{
    public class CreateControllerDeviceDto
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public Guid FieldId { get; set; }
    }
}

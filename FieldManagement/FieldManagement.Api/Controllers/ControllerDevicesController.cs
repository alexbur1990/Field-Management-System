using FieldManagement.Api.DTOs;
using FieldManagement.Api.Models;
using FieldManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FieldManagement.Api.Controllers
{

    [ApiController]
    [Route("api/controllers")]
    public class ControllerDevicesController : ControllerBase
    {
        private readonly ControllerDeviceService _service;

        private static readonly Guid CurrentUserId =
            Guid.Parse("11111111-1111-1111-1111-111111111111");

        public ControllerDevicesController(ControllerDeviceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ControllerDeviceDto>>> Get()
        {
            var devices = await _service.GetForUserAsync(CurrentUserId);

            return Ok(devices.Select(d => new ControllerDeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                Type = d.Type,
                FieldId = d.FieldId
            }));
        }

        [HttpPost]
        public async Task<ActionResult<ControllerDeviceDto>> Create(CreateControllerDeviceDto dto)
        {
            var device = new ControllerDevice
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Type = dto.Type,
                FieldId = dto.FieldId,
                OwnerUserId = CurrentUserId
            };

            await _service.CreateAsync(device);

            return Ok(new ControllerDeviceDto
            {
                Id = device.Id,
                Name = device.Name,
                Type = device.Type,
                FieldId = device.FieldId
            });
        }
    }
}

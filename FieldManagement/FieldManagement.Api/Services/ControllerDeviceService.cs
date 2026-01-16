using FieldManagement.Api.Data;
using FieldManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldManagement.Api.Services
{
    public class ControllerDeviceService
    {
        private readonly AppDbContext _db;

        public ControllerDeviceService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ControllerDevice>> GetForUserAsync(Guid userId)
        {
            return await _db.ControllerDevices
                .Where(c => c.OwnerUserId == userId)
                .ToListAsync();
        }

        public async Task<ControllerDevice> CreateAsync(ControllerDevice device)
        {
            var fieldExists = await _db.Fields.AnyAsync(f =>
                f.Id == device.FieldId &&
                f.OwnerUserId == device.OwnerUserId);

            if (!fieldExists)
                throw new InvalidOperationException("Field does not exist or access denied");

            _db.ControllerDevices.Add(device);
            await _db.SaveChangesAsync();
            return device;
        }
    }
}

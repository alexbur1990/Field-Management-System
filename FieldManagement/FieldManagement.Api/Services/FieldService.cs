using FieldManagement.Api.Data;
using FieldManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldManagement.Api.Services
{
    public class FieldService
    {
        private readonly AppDbContext _db;

        public FieldService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Field>> GetForUserAsync(Guid userId)
        {
            return await _db.Fields
                .Where(f => f.OwnerUserId == userId)
                .ToListAsync();
        }

        public async Task<Field?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _db.Fields
                .FirstOrDefaultAsync(f => f.Id == id && f.OwnerUserId == userId);
        }

        public async Task<Field> CreateAsync(Field field)
        {
            _db.Fields.Add(field);
            await _db.SaveChangesAsync();
            return field;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var field = await GetByIdAsync(id, userId);
            if (field == null) return false;

            _db.Fields.Remove(field);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

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

        public async Task<Field> CreateAsync(Field field)
        {
            _db.Fields.Add(field);
            await _db.SaveChangesAsync();
            return field;
        }
    }
}

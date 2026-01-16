using FieldManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldManagement.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Field> Fields => Set<Field>();
    }
}
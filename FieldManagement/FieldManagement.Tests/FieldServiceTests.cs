using FieldManagement.Api.Models;
using FieldManagement.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldManagement.Tests
{
    public class FieldServiceTests
    {
        [Fact]
        public async Task CreateAsync_Creates_Field_For_User()
        {
            var db = TestDbContextFactory.Create();
            var service = new FieldService(db);

            var userId = Guid.NewGuid();

            var field = new Field
            {
                Id = Guid.NewGuid(),
                Name = "Test Field",
                Area = 10,
                OwnerUserId = userId
            };

            await service.CreateAsync(field);

            var fields = await service.GetForUserAsync(userId);

            Assert.Single(fields);
            Assert.Equal("Test Field", fields[0].Name);
        }

        [Fact]
        public async Task GetForUserAsync_Does_Not_Return_Other_Users_Fields()
        {
            var db = TestDbContextFactory.Create();
            var service = new FieldService(db);

            var userA = Guid.NewGuid();
            var userB = Guid.NewGuid();

            await service.CreateAsync(new Field
            {
                Id = Guid.NewGuid(),
                Name = "User A Field",
                Area = 5,
                OwnerUserId = userA
            });

            await service.CreateAsync(new Field
            {
                Id = Guid.NewGuid(),
                Name = "User B Field",
                Area = 7,
                OwnerUserId = userB
            });

            var fieldsForUserA = await service.GetForUserAsync(userA);

            Assert.Single(fieldsForUserA);
            Assert.Equal("User A Field", fieldsForUserA[0].Name);
        }
    }
}

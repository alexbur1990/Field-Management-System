using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldManagement.Api.Models;
using FieldManagement.Api.Services;

namespace FieldManagement.Tests
{
    public class ControllerDeviceServiceTests
    {
        [Fact]
        public async Task CreateAsync_Creates_Device_For_Owned_Field()
        {
            // Arrange
            var db = TestDbContextFactory.Create();
            var service = new ControllerDeviceService(db);

            var userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Email = "test@example.com",
                IsAdmin = false
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();   // ✅ IMPORTANT

            var field = new Field
            {
                Id = Guid.NewGuid(),
                Name = "Owned Field",
                Area = 10,
                OwnerUserId = userId
            };

            db.Fields.Add(field);
            await db.SaveChangesAsync();   // ✅ IMPORTANT

            var device = new ControllerDevice
            {
                Id = Guid.NewGuid(),
                Name = "Irrigation Controller",
                Type = "Irrigation",
                FieldId = field.Id,
                OwnerUserId = userId
            };

            // Act
            await service.CreateAsync(device);

            var devices = await service.GetForUserAsync(userId);

            // Assert
            Assert.Single(devices);
            Assert.Equal("Irrigation Controller", devices[0].Name);
        }


        [Fact]
        public async Task CreateAsync_Throws_When_Field_Not_Owned_By_User()
        {
            var db = TestDbContextFactory.Create();
            var service = new ControllerDeviceService(db);

            var ownerUserId = Guid.NewGuid();
            var otherUserId = Guid.NewGuid();

            db.Users.AddRange(
                new User { Id = ownerUserId, Email = "owner@test.com" },
                new User { Id = otherUserId, Email = "other@test.com" }
            );
            await db.SaveChangesAsync();   // ✅

            var field = new Field
            {
                Id = Guid.NewGuid(),
                Name = "Other User Field",
                Area = 20,
                OwnerUserId = ownerUserId
            };

            db.Fields.Add(field);
            await db.SaveChangesAsync();   // ✅

            var device = new ControllerDevice
            {
                Id = Guid.NewGuid(),
                Name = "Sensor Controller",
                Type = "Sensor",
                FieldId = field.Id,
                OwnerUserId = otherUserId
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.CreateAsync(device));
        }

    }
}

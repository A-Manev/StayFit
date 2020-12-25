namespace StayFit.Services.Data.Tests.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using StayFit.Data;

    public class BaseServiceTests
    {
        public BaseServiceTests()
        {
            new MapperInitializationProfile();
        }

        public static ApplicationDbContext GetDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);

            return db;
        }
    }
}

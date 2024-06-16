using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Base.Infrastructure.Persistence;

namespace Base.UnitTests.Infrastructure;

public class SharedDatabaseFixture: IDisposable {

    public BaseDbContext DbContext { get; }

    public SharedDatabaseFixture() {
        var dbContextOptions = new DbContextOptionsBuilder<BaseDbContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;
        DbContext = new BaseDbContext(dbContextOptions);

        SeedData();
    }

    public void SeedData() {
        if(!DbContext.Users.Any()) {
            DbContext.Users.Add(new() { 
                Id       = 1, 
                Name     = "Daniel", 
                LastName = "Cárdenas", 
                Email    = "dcardenas@gmail.com", 
                Password = "Daniel2024."
            });
            
            DbContext.SaveChanges();
        }
    }

    public void Dispose(){
        DbContext.Dispose();
    }
}
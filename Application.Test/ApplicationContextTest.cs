using System.Diagnostics;
using Application.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Test;

public class ApplicationContextTest
{ 
    private AppointmentContext GetDatabase()
    {
        // Create in-memory SQLite database
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<AppointmentContext>()
            .UseSqlite(connection)
            .LogTo(message => Debug.WriteLine(message), LogLevel.Information)
            .EnableSensitiveDataLogging()
            .Options;

        var db = new AppointmentContext(options);
        Debug.WriteLine(db.Database.GenerateCreateScript());
        
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        
        return db;
    }
    
    
    
    [Fact]
    public void DatabaseSuccessTest()
    {
        using var db = GetDatabase();
        // Assert.True(db.Database.CanConnect());
    }
}
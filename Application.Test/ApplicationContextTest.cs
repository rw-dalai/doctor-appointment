using System.Diagnostics;
using Application.Infrastructure;
using Application.Models;
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
        // try-with-resources
        using var db = GetDatabase();
        Assert.True(db.Database.CanConnect());
    }

    [Fact]
    public void AddPatientTest()
    {
        using var db = GetDatabase();
        
        // Given
        var insuranceNr = new InsuranceNumber("1234567890");
        var phoneNr = new PhoneNumber("+431234567890");
        var patient = new Patient("Ana", "Suwarti", insuranceNr, phoneNr);
        
        // When
        db.Patients.Add(patient);
        // INSERT INTO "Patient" ("Firstname", "InsuranceNumber", "Lastname", "Mobile")
        // VALUES (@p0, @p1, @p2, @p3)
        // RETURNING "Id";
        db.SaveChanges();
        db.ChangeTracker.Clear();
        
        // Then
        // SELECT "p"."Id", "p"."Firstname", "p"."InsuranceNumber", "p"."Lastname", "p"."Mobile"
        // FROM "Patient" AS "p"
        // WHERE "p"."Id" = @p
        // LIMIT 1
        var retrievedPatient = db.Patients.Find(patient.Id);
        Assert.NotNull(retrievedPatient);
    }
}
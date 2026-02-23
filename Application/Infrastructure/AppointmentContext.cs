using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure;


/**
 * CREATE TABLE "Doctors" (
      "Id" INTEGER NOT NULL CONSTRAINT "PK_Doctors" PRIMARY KEY AUTOINCREMENT,
      "Firstname" TEXT NOT NULL,
      "Lastname" TEXT NOT NULL,
      "Email" TEXT NOT NULL
      );
      
    CREATE TABLE "Patients" (
          "Id" INTEGER NOT NULL CONSTRAINT "PK_Patients" PRIMARY KEY AUTOINCREMENT,
          "Firstname" TEXT NOT NULL,
          "Lastname" TEXT NOT NULL,
          "InsuranceNumber" VARCHAR2 NOT NULL,
          "Mobile" TEXT NULL
      );
      
      CREATE TABLE "Appointment" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_Appointment" PRIMARY KEY AUTOINCREMENT,
            "Date" TEXT NOT NULL,
            "Created" TEXT NOT NULL,
            "PatientId" INTEGER NOT NULL,
            CONSTRAINT "FK_Appointment_Patient_PatientId" FOREIGN KEY ("PatientId") REFERENCES "Patient" ("Id") ON DELETE CASCADE
        );
        
        CREATE TABLE "AppointmentState" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_AppointmentState" PRIMARY KEY AUTOINCREMENT,
            "AppointmentId" INTEGER NOT NULL,
            "Created" TEXT NOT NULL,
            "Type" TEXT NOT NULL,
            "DoctorId" INTEGER NULL,
            "PlannedSlot_start" TEXT NULL,
            "PlannedSlot_end" TEXT NULL,
            "Infotext" TEXT NULL,
            CONSTRAINT "FK_AppointmentState_Appointment_AppointmentId" FOREIGN KEY ("AppointmentId") REFERENCES "Appointment" ("Id") ON DELETE CASCADE,
            CONSTRAINT "FK_AppointmentState_Doctors_DoctorId" FOREIGN KEY ("DoctorId") REFERENCES "Doctors" ("Id") ON DELETE CASCADE
);
 */
public class AppointmentContext(DbContextOptions options ) : DbContext(options)
{
    /**
 * CREATE TABLE "Doctors" (
      "Id" INTEGER NOT NULL CONSTRAINT "PK_Doctors" PRIMARY KEY AUTOINCREMENT,
      "Firstname" TEXT NOT NULL,
      "Lastname" TEXT NOT NULL,
      "Email" TEXT NOT NULL
      );
      
    CREATE TABLE "Patients" (
          "Id" INTEGER NOT NULL CONSTRAINT "PK_Patients" PRIMARY KEY AUTOINCREMENT,
          "Firstname" TEXT NOT NULL,
          "Lastname" TEXT NOT NULL,
          "InsuranceNumber" VARCHAR2 NOT NULL,
          "Mobile" TEXT NULL
      );
      
      CREATE TABLE "Appointment" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_Appointment" PRIMARY KEY AUTOINCREMENT,
            "Date" TEXT NOT NULL,
            "Created" TEXT NOT NULL,
            "PatientId" INTEGER NOT NULL,
            CONSTRAINT "FK_Appointment_Patient_PatientId" FOREIGN KEY ("PatientId") REFERENCES "Patient" ("Id") ON DELETE CASCADE
        );
        
        CREATE TABLE "AppointmentState" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_AppointmentState" PRIMARY KEY AUTOINCREMENT,
            "AppointmentId" INTEGER NOT NULL,
            "Created" TEXT NOT NULL,
            "Type" TEXT NOT NULL,
            "DoctorId" INTEGER NULL,
            "PlannedSlot_start" TEXT NULL,
            "PlannedSlot_end" TEXT NULL,
            "Infotext" TEXT NULL,
            CONSTRAINT "FK_AppointmentState_Appointment_AppointmentId" FOREIGN KEY ("AppointmentId") REFERENCES "Appointment" ("Id") ON DELETE CASCADE,
            CONSTRAINT "FK_AppointmentState_Doctors_DoctorId" FOREIGN KEY ("DoctorId") REFERENCES "Doctors" ("Id") ON DELETE CASCADE
);
 */
    // --- DB Tables --

    public DbSet<Doctor> Doctors => Set<Doctor>();
    
    public DbSet<Patient> Patients => Set<Patient>();
    
    public DbSet<Appointment> Appointments => Set<Appointment>();
    
    public DbSet<AppointmentState> AppointmentStates => Set<AppointmentState>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Tablennamen
        modelBuilder.Entity<Doctor>().ToTable("Doctor");
        modelBuilder.Entity<Patient>().ToTable("Patient");
        modelBuilder.Entity<Appointment>().ToTable("Appointment");
        modelBuilder.Entity<AppointmentState>().ToTable("AppointmentState");
        
        
        // 2. Relationships 
        // (1:1, bidirektional, parent: Appointment, child: AppointmentState)
        modelBuilder.Entity<Appointment>()
            .HasOne<AppointmentState>(a => a.CurrentState)
            .WithOne(s => s.Appointment)
            .HasForeignKey<AppointmentState>("AppointmentId");
            // .HasForeignKey<AppointmentState>(nameof(AppointmentState.AppointmentId));
        
        
        // 3. Value Objects, Rich Types, Enums
        modelBuilder.Entity<Patient>()
            .Property<InsuranceNumber>(p => p.InsuranceNumber)
            .HasConversion(
                number => number.Value,             // C# -> DB
                value => new InsuranceNumber(value) // DB -> C#
            );
        
        modelBuilder.Entity<Patient>()
            .Property<PhoneNumber>(p => p.Mobile)
            .HasConversion(
                number => number.Value,         // C# -> DB
                value => new PhoneNumber(value) // DB -> C#
            );


        modelBuilder.Entity<ConfirmedAppointmentState>()
            .OwnsOne<TimeSlot>(c => c.PlannedSlot);


        // 4. Indizes
        modelBuilder.Entity<Doctor>().HasIndex("Email").IsUnique();

        modelBuilder.Entity<Appointment>().HasIndex("Date", "PatientId").IsUnique();

        // modelBuilder.Entity<Appointment>()
            // .HasIndex( a => new { a.Date, a.PatientId }).IsUnique();


        // 5. Inheritance (TPH -> Table per Hierarchy)
        modelBuilder.Entity<AppointmentState>()
            .HasDiscriminator<string>("Type")
            .HasValue<ConfirmedAppointmentState>("Confirmed")
            .HasValue<CancelledAppointmentState>("Cancelled");
    }
}
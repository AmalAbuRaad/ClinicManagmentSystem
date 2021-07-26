using Clinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Data
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Specialization>().HasData(
                new {Id= (long) 1, SpecializationName="Sp1"},
                new {Id= (long) 2, SpecializationName="Sp2"}
                );
;
        }

        public DbSet<Doctor> Doctors { get; set; } //Create table in Database with attributes in Doctor Class
        public DbSet<Patient> Patients { get; set; } //Create table in DB Patient Class
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }

    }
}

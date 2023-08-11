using Common.Infrastructure.Repositories.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UI.Domain.Models.ClinicalDataAggregate;
using UI.Domain.Models.DemographicsAggregate;
using UI.Domain.Models.PatientAggregate;
using UI.Domain.Models.SymptomsAggregate;

namespace UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<DemographicsModel> Demographics { get; set; }
        public DbSet<SymptomsModel> Symptoms { get; set; }
        public DbSet<ClinicalDataModel> ClinicalData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l=>l.UserId);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r=> r.RoleId);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t=> t.UserId);
            modelBuilder.Entity<PatientModel>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<PatientModel>()
                .Ignore(p => p.ModifiedProperties);

            modelBuilder.Entity<DemographicsModel>().HasKey(d => d.Id);
            
            modelBuilder.Entity<SymptomsModel>().HasKey(s => s.Id);
            modelBuilder.Entity<SymptomsModel>()
                .Property(e => e.Options)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<ClinicalDataModel>().HasKey(d => d.Id);

        }
    }
}


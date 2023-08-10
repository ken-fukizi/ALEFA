using Common.Infrastructure.Repositories.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UI.Domain.Models.PatientAggregate;

namespace UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PatientModel> Patients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l=>l.UserId);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r=> r.RoleId);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t=> t.UserId);
            modelBuilder.Entity<PatientModel>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<PatientModel>()
                .Ignore(p => p.ModifiedProperties);
        }
    }
}


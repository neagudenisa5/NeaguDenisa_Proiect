using Microsoft.EntityFrameworkCore;
using NeaguDenisa_Proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeaguDenisa_Proiect.Data
{
    public class SpitalContext : DbContext
    {
        public SpitalContext(DbContextOptions<SpitalContext> options) : base(options)
        {
        }
        public DbSet<Pacient> Pacienti { get; set; }
        public DbSet<Programare> Programari { get; set; }
        public DbSet<Medic> Medici { get; set; }
        public DbSet<Spital> Spitale { get; set; }
        public DbSet<SpitalMedic> SpitalMedici { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacient>().ToTable("Pacient");
            modelBuilder.Entity<Programare>().ToTable("Programare");
            modelBuilder.Entity<Medic>().ToTable("Medic");
            modelBuilder.Entity<Spital>().ToTable("Spital");
            modelBuilder.Entity<SpitalMedic>().ToTable("SpitalMedic");
            modelBuilder.Entity<SpitalMedic>()
                        .HasKey(c => new { c.MedicID, c.SpitalID });
        }
    }

}

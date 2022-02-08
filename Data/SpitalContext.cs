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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacient>().ToTable("Pacient");
            modelBuilder.Entity<Programare>().ToTable("Programare");
            modelBuilder.Entity<Medic>().ToTable("Medic");
        }
    }

}

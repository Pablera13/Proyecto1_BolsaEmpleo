using Microsoft.EntityFrameworkCore;
using Proyecto1_BolsaEmpleo.Models;

namespace Proyecto1_BolsaEmpleo.Data
{
    public class MyApiContext : DbContext
    {

        public MyApiContext(DbContextOptions<MyApiContext> options)
           : base(options)
        {

        }

        // Relaciones de uno a muchos
        public DbSet<Candidato> Candidato { get; set; } = default!;
        public DbSet<Formacion> Formacion { get; set; } = default!;
        public DbSet<Empresa> Empresa { get; set; } = default!;
        public DbSet<Oferta> Oferta { get; set; } = default!;

        // Relaciones de muchos a muchos
        public DbSet<Habilidad> Habilidad { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // uno a muchos
            modelBuilder.Entity<Formacion>()
            .HasOne(formacion => formacion.Candidato)
            .WithMany(candidato => candidato.formaciones)
            .HasForeignKey(k => k.CandidatoId);

            modelBuilder.Entity<Oferta>()
            .HasOne(oferta => oferta.Empresa)
            .WithMany(empresa => empresa.ofertas)
            .HasForeignKey(k => k.EmpresaId);


            // muchos a muchos
            modelBuilder.Entity<Candidato>()
           .HasMany(candidato => candidato.habilidades)
           .WithMany(habilidades => habilidades.candidatos)
           .UsingEntity(j => j.ToTable("CandidatoHabilidad"));


            modelBuilder.Entity<Empresa>()
           .HasMany(empresa => empresa.habilidades)
           .WithMany(habilidades => habilidades.empresas)
           .UsingEntity(j => j.ToTable("EmpresaHabilidad"));

        }

    }

}


using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
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
        public DbSet<Habilidad> Habilidad { get; set; } = default!;

        // Relaciones de muchos a muchos

        public DbSet<CandidatoHabilidad> CandidatoHabilidad { get; set; } = default!;
        public DbSet<OfertaHabilidad> OfertaHabilidad { get; set; } = default!;
        public DbSet<CandidatoOferta> CandidatoOferta { get; set; } = default!;


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

            // CandidatoHabilidad

            modelBuilder.Entity<CandidatoHabilidad>()
            .HasKey(ch => new { ch.CandidatoId, ch.HabilidadId });

            modelBuilder.Entity<CandidatoHabilidad>()
                .HasOne(ch => ch.Candidato)
                .WithMany(c => c.CandidatoHabilidades)
                .HasForeignKey(ch => ch.CandidatoId);

            modelBuilder.Entity<CandidatoHabilidad>()
                .HasOne(ch => ch.Habilidad)
                .WithMany(h => h.CandidatoHabilidades)
                .HasForeignKey(ch => ch.HabilidadId);

            // OfertaHabilidad

            modelBuilder.Entity<OfertaHabilidad>()
            .HasKey(ch => new { ch.OfertaId, ch.HabilidadId });

            modelBuilder.Entity<OfertaHabilidad>()
                .HasOne(ch => ch.Oferta)
                .WithMany(c => c.OfertaHabilidades)
                .HasForeignKey(ch => ch.OfertaId);

            modelBuilder.Entity<OfertaHabilidad>()
                .HasOne(ch => ch.Habilidad)
                .WithMany(h => h.OfertaHabilidades)
                .HasForeignKey(ch => ch.HabilidadId);

            // CandidatoOferta

            modelBuilder.Entity<CandidatoOferta>()
            .HasKey(ch => new { ch.CandidatoId, ch.OfertaId });

            modelBuilder.Entity<CandidatoOferta>()
                .HasOne(ch => ch.Candidato)
                .WithMany(c => c.CandidatoOfertas)
                .HasForeignKey(ch => ch.CandidatoId);

            modelBuilder.Entity<CandidatoOferta>()
                .HasOne(ch => ch.Oferta)
                .WithMany(h => h.CandidatoOfertas)
                .HasForeignKey(ch => ch.CandidatoId);




        }



    }

}


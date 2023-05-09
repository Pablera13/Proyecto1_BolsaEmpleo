﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proyecto1_BolsaEmpleo.Data;

#nullable disable

namespace Proyecto1_BolsaEmpleo.Migrations
{
    [DbContext(typeof(MyApiContext))]
    partial class MyApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Candidato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Apellido2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fecha_Nacimiento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Candidato");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.CandidatoHabilidad", b =>
                {
                    b.Property<int>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<int>("HabilidadId")
                        .HasColumnType("int");

                    b.HasKey("CandidatoId", "HabilidadId");

                    b.HasIndex("HabilidadId");

                    b.ToTable("CandidatoHabilidad");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.CandidatoOferta", b =>
                {
                    b.Property<int>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<int>("OfertaId")
                        .HasColumnType("int");

                    b.HasKey("CandidatoId", "OfertaId");

                    b.ToTable("CandidatoOferta");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Formacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Años_Estudio")
                        .HasColumnType("int");

                    b.Property<int>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<string>("Fecha_Culminacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CandidatoId");

                    b.ToTable("Formacion");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Habilidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Habilidad");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Oferta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Oferta");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.OfertaHabilidad", b =>
                {
                    b.Property<int>("OfertaId")
                        .HasColumnType("int");

                    b.Property<int>("HabilidadId")
                        .HasColumnType("int");

                    b.HasKey("OfertaId", "HabilidadId");

                    b.HasIndex("HabilidadId");

                    b.ToTable("OfertaHabilidad");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.CandidatoHabilidad", b =>
                {
                    b.HasOne("Proyecto1_BolsaEmpleo.Models.Candidato", "Candidato")
                        .WithMany("CandidatoHabilidades")
                        .HasForeignKey("CandidatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyecto1_BolsaEmpleo.Models.Habilidad", "Habilidad")
                        .WithMany("CandidatoHabilidades")
                        .HasForeignKey("HabilidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidato");

                    b.Navigation("Habilidad");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.CandidatoOferta", b =>
                {
                    b.HasOne("Proyecto1_BolsaEmpleo.Models.Candidato", "Candidato")
                        .WithMany("CandidatoOfertas")
                        .HasForeignKey("CandidatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyecto1_BolsaEmpleo.Models.Oferta", "Oferta")
                        .WithMany("CandidatoOfertas")
                        .HasForeignKey("CandidatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidato");

                    b.Navigation("Oferta");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Formacion", b =>
                {
                    b.HasOne("Proyecto1_BolsaEmpleo.Models.Candidato", "Candidato")
                        .WithMany("formaciones")
                        .HasForeignKey("CandidatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidato");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Oferta", b =>
                {
                    b.HasOne("Proyecto1_BolsaEmpleo.Models.Empresa", "Empresa")
                        .WithMany("ofertas")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.OfertaHabilidad", b =>
                {
                    b.HasOne("Proyecto1_BolsaEmpleo.Models.Habilidad", "Habilidad")
                        .WithMany("OfertaHabilidades")
                        .HasForeignKey("HabilidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyecto1_BolsaEmpleo.Models.Oferta", "Oferta")
                        .WithMany("OfertaHabilidades")
                        .HasForeignKey("OfertaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habilidad");

                    b.Navigation("Oferta");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Candidato", b =>
                {
                    b.Navigation("CandidatoHabilidades");

                    b.Navigation("CandidatoOfertas");

                    b.Navigation("formaciones");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Empresa", b =>
                {
                    b.Navigation("ofertas");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Habilidad", b =>
                {
                    b.Navigation("CandidatoHabilidades");

                    b.Navigation("OfertaHabilidades");
                });

            modelBuilder.Entity("Proyecto1_BolsaEmpleo.Models.Oferta", b =>
                {
                    b.Navigation("CandidatoOfertas");

                    b.Navigation("OfertaHabilidades");
                });
#pragma warning restore 612, 618
        }
    }
}

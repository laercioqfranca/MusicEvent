﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicEvent.Infra.Data.Context;

#nullable disable

namespace MusicEvent.Infra.Data.Migrations
{
    [DbContext(typeof(MusicEventContext))]
    [Migration("20231028014621_Table_Evento")]
    partial class Table_Evento
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MusicEvent.Domain.Models.Administracao.LogHistorico", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EntidadeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NomeEntidade")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<short>("TipoLog")
                        .HasColumnType("smallint");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("LogHistorico");
                });

            modelBuilder.Entity("MusicEvent.Domain.Models.Autenticacao.ClaimPerfil", b =>
                {
                    b.Property<Guid>("IdClaim")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdPerfil")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdClaim", "IdPerfil");

                    b.HasIndex("IdPerfil");

                    b.ToTable("ClaimPerfil");
                });

            modelBuilder.Entity("MusicEvent.Domain.Models.Autenticacao.ClaimUsuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Excluido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.ToTable("ClaimUsuario");
                });

            modelBuilder.Entity("MusicEvent.Domain.Models.Autenticacao.PerfilUsuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Excluido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("IdTipoPerfil")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PerfilUsuario");
                });

            modelBuilder.Entity("MusicEvent.Domain.Models.Autenticacao.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Excluido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid?>("IdPerfil")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("RedefinirSenha")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IdPerfil");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("MusicEvent.Domain.Models.Evento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("MusicEvent.Domain.Models.Autenticacao.ClaimPerfil", b =>
                {
                    b.HasOne("MusicEvent.Domain.Models.Autenticacao.ClaimUsuario", "Claim")
                        .WithMany()
                        .HasForeignKey("IdClaim")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MusicEvent.Domain.Models.Autenticacao.PerfilUsuario", "Perfil")
                        .WithMany("ClaimsPerfil")
                        .HasForeignKey("IdPerfil")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Claim");

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("MusicEvent.Domain.Models.Autenticacao.Usuario", b =>
                {
                    b.HasOne("MusicEvent.Domain.Models.Autenticacao.PerfilUsuario", "Perfil")
                        .WithMany()
                        .HasForeignKey("IdPerfil")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("MusicEvent.Domain.Models.Autenticacao.PerfilUsuario", b =>
                {
                    b.Navigation("ClaimsPerfil");
                });
#pragma warning restore 612, 618
        }
    }
}

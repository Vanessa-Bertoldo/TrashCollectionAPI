﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using TrashCollectionAPI.Data.Contexts;

#nullable disable

namespace TrashCollectionAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TrashCollectionAPI.Models.CaminhaoModel", b =>
                {
                    b.Property<int>("IdCaminhao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCaminhao"));

                    b.Property<int>("HNumeroMaxCapacidade")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("IdStatus")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("NumeroCapacidade")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("IdCaminhao");

                    b.HasIndex("IdStatus");

                    b.ToTable("Caminhao", (string)null);
                });

            modelBuilder.Entity("TrashCollectionAPI.Models.ColetaModel", b =>
                {
                    b.Property<int>("IdColeta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdColeta"));

                    b.Property<DateTime>("DataColeta")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("NomeBairro")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<double>("NumeroVolume")
                        .HasColumnType("BINARY_DOUBLE");

                    b.HasKey("IdColeta");

                    b.ToTable("Coleta", (string)null);
                });

            modelBuilder.Entity("TrashCollectionAPI.Models.RotaModel", b =>
                {
                    b.Property<int>("IdRota")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRota"));

                    b.Property<string>("DescricaoRota")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("IdColeta")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("NomeRota")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("IdRota");

                    b.HasIndex("IdColeta");

                    b.ToTable("Rota", (string)null);
                });

            modelBuilder.Entity("TrashCollectionAPI.Models.StatusModel", b =>
                {
                    b.Property<int>("IdStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStatus"));

                    b.Property<string>("NomeStatus")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("IdStatus");

                    b.ToTable("Status", (string)null);
                });

            modelBuilder.Entity("TrashCollectionAPI.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Role")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("UserId");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("TrashCollectionAPI.Models.CaminhaoModel", b =>
                {
                    b.HasOne("TrashCollectionAPI.Models.StatusModel", "Status")
                        .WithMany()
                        .HasForeignKey("IdStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("TrashCollectionAPI.Models.RotaModel", b =>
                {
                    b.HasOne("TrashCollectionAPI.Models.ColetaModel", "Coleta")
                        .WithMany("Rotas")
                        .HasForeignKey("IdColeta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coleta");
                });

            modelBuilder.Entity("TrashCollectionAPI.Models.ColetaModel", b =>
                {
                    b.Navigation("Rotas");
                });
#pragma warning restore 612, 618
        }
    }
}

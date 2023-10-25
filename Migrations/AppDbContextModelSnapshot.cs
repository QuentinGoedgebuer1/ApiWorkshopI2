﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkShopI2;

#nullable disable

namespace WorkShopI2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WorkShopI2.Models.Mesures.Mesure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AQI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateHeure")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Humidite")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ParkId")
                        .HasColumnType("integer");

                    b.Property<string>("Temperature")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParkId");

                    b.ToTable("Mesures");
                });

            modelBuilder.Entity("WorkShopI2.Models.Parks.Park", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VillesId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VillesId");

                    b.ToTable("Parks");
                });

            modelBuilder.Entity("WorkShopI2.Models.Villes.Ville", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Villes");
                });

            modelBuilder.Entity("WorkShopI2.Models.WeatherForecast.WeatherForecast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("WeatherForecasts");
                });

            modelBuilder.Entity("WorkShopI2.Models.Mesures.Mesure", b =>
                {
                    b.HasOne("WorkShopI2.Models.Parks.Park", "Park")
                        .WithMany("Mesures")
                        .HasForeignKey("ParkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Park");
                });

            modelBuilder.Entity("WorkShopI2.Models.Parks.Park", b =>
                {
                    b.HasOne("WorkShopI2.Models.Villes.Ville", "Villes")
                        .WithMany("Parks")
                        .HasForeignKey("VillesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villes");
                });

            modelBuilder.Entity("WorkShopI2.Models.Parks.Park", b =>
                {
                    b.Navigation("Mesures");
                });

            modelBuilder.Entity("WorkShopI2.Models.Villes.Ville", b =>
                {
                    b.Navigation("Parks");
                });
#pragma warning restore 612, 618
        }
    }
}

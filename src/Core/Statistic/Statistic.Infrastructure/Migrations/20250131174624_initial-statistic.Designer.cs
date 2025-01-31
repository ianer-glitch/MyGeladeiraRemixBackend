﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Statistic.Infrastructure;

#nullable disable

namespace Statistic.Infrastructure.Migrations
{
    [DbContext(typeof(StatisticContext))]
    [Migration("20250131174624_initial-statistic")]
    partial class initialstatistic
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Statistic.Domain.ExpiredStatistic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Inclusion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<float>("ItemWeight")
                        .HasColumnType("real");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("StatisticId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserInclusionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserModifiedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StatisticId");

                    b.ToTable("ExpiredStatistics");
                });

            modelBuilder.Entity("Statistic.Domain.Statistic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Inclusion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserInclusionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserModifiedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("Statistic.Domain.UserFoodWasteIndex", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Inclusion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Index")
                        .HasColumnType("real");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserInclusionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserModifiedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserFoodWasteIndexes");
                });

            modelBuilder.Entity("Statistic.Domain.ExpiredStatistic", b =>
                {
                    b.HasOne("Statistic.Domain.Statistic", "Statistic")
                        .WithMany()
                        .HasForeignKey("StatisticId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Statistic");
                });
#pragma warning restore 612, 618
        }
    }
}

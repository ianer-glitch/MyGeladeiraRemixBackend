﻿// <auto-generated />
using System;
using Fridge.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fridge.Infrastructure.Migrations
{
    [DbContext(typeof(FridgeContext))]
    [Migration("20250128212905_item-fridges-and-fridgeITems")]
    partial class itemfridgesandfridgeITems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Fridge.Domain.Fridges.Fridge", b =>
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

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserInclusionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserModifiedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Fridges");
                });

            modelBuilder.Entity("Fridge.Domain.Items.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IconName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Inclusion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("MinimunQuantity")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserInclusionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserModifiedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Items", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Fridge.Domain.Fridges.FridgeItem", b =>
                {
                    b.HasBaseType("Fridge.Domain.Items.Item");

                    b.Property<Guid>("FridgeId")
                        .HasColumnType("uuid");

                    b.HasIndex("FridgeId");

                    b.ToTable("FridgeItems", (string)null);
                });

            modelBuilder.Entity("Fridge.Domain.Fridges.FridgeItem", b =>
                {
                    b.HasOne("Fridge.Domain.Fridges.Fridge", "Fridge")
                        .WithMany()
                        .HasForeignKey("FridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fridge.Domain.Items.Item", null)
                        .WithOne()
                        .HasForeignKey("Fridge.Domain.Fridges.FridgeItem", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fridge");
                });
#pragma warning restore 612, 618
        }
    }
}

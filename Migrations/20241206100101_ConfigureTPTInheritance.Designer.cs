﻿// <auto-generated />
using System;
using LexiconMiniProject03;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LexiconMiniProject03.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20241206100101_ConfigureTPTInheritance")]
    partial class ConfigureTPTInheritance
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LexiconMiniProject03.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.Property<decimal>("PriceUSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Assets", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("LexiconMiniProject03.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("LexiconMiniProject03.Computer", b =>
                {
                    b.HasBaseType("LexiconMiniProject03.Asset");

                    b.ToTable("Computers", (string)null);
                });

            modelBuilder.Entity("LexiconMiniProject03.Phone", b =>
                {
                    b.HasBaseType("LexiconMiniProject03.Asset");

                    b.ToTable("Phones", (string)null);
                });

            modelBuilder.Entity("LexiconMiniProject03.Asset", b =>
                {
                    b.HasOne("LexiconMiniProject03.Office", "OfficeLocation")
                        .WithMany("Assets")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OfficeLocation");
                });

            modelBuilder.Entity("LexiconMiniProject03.Computer", b =>
                {
                    b.HasOne("LexiconMiniProject03.Asset", null)
                        .WithOne()
                        .HasForeignKey("LexiconMiniProject03.Computer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LexiconMiniProject03.Phone", b =>
                {
                    b.HasOne("LexiconMiniProject03.Asset", null)
                        .WithOne()
                        .HasForeignKey("LexiconMiniProject03.Phone", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LexiconMiniProject03.Office", b =>
                {
                    b.Navigation("Assets");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using IT_Talent_Program.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IT_Talent_Program.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240412193538_initMig")]
    partial class initMig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.16");

            modelBuilder.Entity("IT_Talent_Program.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Admin")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RevokedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RevokedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ffd76aa5-653c-4d74-b954-2ae48aa8bb0f"),
                            Admin = true,
                            CreatedBy = "admin",
                            CreatedOn = new DateTime(2024, 4, 12, 19, 35, 38, 123, DateTimeKind.Utc).AddTicks(1394),
                            Gender = 2,
                            Login = "admin",
                            Name = "admin",
                            Password = "admin123*"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

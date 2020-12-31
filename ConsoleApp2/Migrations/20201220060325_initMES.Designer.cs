﻿// <auto-generated />
using System;
using ConsoleApp2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConsoleApp2.Migrations
{
    [DbContext(typeof(MySqlDbContext))]
    [Migration("20201220060325_initMES")]
    partial class initMES
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ConsoleApp2.UserInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("CreateUser")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasMaxLength(20);

                    b.Property<string>("PassWord")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int>("UserCount")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
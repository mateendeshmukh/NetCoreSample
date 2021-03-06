﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyFirstCore.Data;

namespace MyFirstCore.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("MyFirstCore.Models.User", b =>
                {
                    b.Property<int>("USerId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("PassowrdSalt");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<string>("UserName");

                    b.HasKey("USerId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyFirstCore.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });
#pragma warning restore 612, 618
        }
    }
}

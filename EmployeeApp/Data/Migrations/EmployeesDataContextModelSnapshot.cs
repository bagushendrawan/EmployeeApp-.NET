﻿// <auto-generated />
using System;
using EmployeeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeApp.Data.Migrations
{
    [DbContext(typeof(EmployeesDataContext))]
    partial class EmployeesDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.6.24327.4");

            modelBuilder.Entity("EmployeeApp.Entities.EmployeesEntities", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("Birth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("NIK");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            NIK = "1234",
                            Address = "Jakarta",
                            Birth = new DateOnly(2002, 6, 7),
                            Country = "Indonesia",
                            Gender = "Male",
                            Name = "John Doe"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

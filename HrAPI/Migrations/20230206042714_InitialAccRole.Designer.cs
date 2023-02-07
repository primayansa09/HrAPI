﻿// <auto-generated />
using System;
using HrAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HrAPI.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230206042714_InitialAccRole")]
    partial class InitialAccRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HrAPI.Model.AccountRoles", b =>
                {
                    b.Property<string>("AccountNIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("AccountNIK", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AccountRoles");
                });

            modelBuilder.Entity("HrAPI.Model.Accounts", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NIK");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("HrAPI.Model.Departements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Manager_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Manager_Id")
                        .IsUnique()
                        .HasFilter("[Manager_Id] IS NOT NULL");

                    b.ToTable("Departements");
                });

            modelBuilder.Entity("HrAPI.Model.Employees", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Departement_Id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manager_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NIK");

                    b.HasIndex("Departement_Id");

                    b.HasIndex("Manager_Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HrAPI.Model.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HrAPI.Model.AccountRoles", b =>
                {
                    b.HasOne("HrAPI.Model.Accounts", "Accounts")
                        .WithMany()
                        .HasForeignKey("AccountNIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HrAPI.Model.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accounts");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("HrAPI.Model.Accounts", b =>
                {
                    b.HasOne("HrAPI.Model.Employees", "Employees")
                        .WithOne("Accounts")
                        .HasForeignKey("HrAPI.Model.Accounts", "NIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HrAPI.Model.Departements", b =>
                {
                    b.HasOne("HrAPI.Model.Employees", "Manager")
                        .WithOne("Manager")
                        .HasForeignKey("HrAPI.Model.Departements", "Manager_Id");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("HrAPI.Model.Employees", b =>
                {
                    b.HasOne("HrAPI.Model.Departements", "Departements")
                        .WithMany("Employees")
                        .HasForeignKey("Departement_Id");

                    b.HasOne("HrAPI.Model.Employees", "ManagerEmployees")
                        .WithMany("EmployeesOfManager")
                        .HasForeignKey("Manager_Id");

                    b.Navigation("Departements");

                    b.Navigation("ManagerEmployees");
                });

            modelBuilder.Entity("HrAPI.Model.Departements", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HrAPI.Model.Employees", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("EmployeesOfManager");

                    b.Navigation("Manager");
                });
#pragma warning restore 612, 618
        }
    }
}

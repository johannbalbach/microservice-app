﻿// <auto-generated />
using System;
using Dictionary.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dictionary.Domain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dictionary.Domain.Entities.DocumentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EducationLevelId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EducationLevelId");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("Dictionary.Domain.Entities.EducationLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EducationLevels");
                });

            modelBuilder.Entity("Dictionary.Domain.Entities.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Dictionary.Domain.Entities.UniversityProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EducationForm")
                        .HasColumnType("integer");

                    b.Property<int>("EducationLevelId")
                        .HasColumnType("integer");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EducationLevelId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("DocumentTypeEducationLevel", b =>
                {
                    b.Property<Guid>("DocumentTypeId")
                        .HasColumnType("uuid");

                    b.Property<int>("NextEducationLevelId")
                        .HasColumnType("integer");

                    b.HasKey("DocumentTypeId", "NextEducationLevelId");

                    b.HasIndex("NextEducationLevelId");

                    b.ToTable("DocumentTypeEducationLevel");
                });

            modelBuilder.Entity("Dictionary.Domain.Entities.DocumentType", b =>
                {
                    b.HasOne("Dictionary.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("Dictionary.Domain.Entities.UniversityProgram", b =>
                {
                    b.HasOne("Dictionary.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dictionary.Domain.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationLevel");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("DocumentTypeEducationLevel", b =>
                {
                    b.HasOne("Dictionary.Domain.Entities.DocumentType", null)
                        .WithMany()
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dictionary.Domain.Entities.EducationLevel", null)
                        .WithMany()
                        .HasForeignKey("NextEducationLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

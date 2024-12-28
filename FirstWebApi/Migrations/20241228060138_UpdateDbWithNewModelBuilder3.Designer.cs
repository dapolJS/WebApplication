﻿// <auto-generated />
using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FirstWebApi.Migrations
{
    [DbContext(typeof(DataContextEF))]
    [Migration("20241228060138_UpdateDbWithNewModelBuilder3")]
    partial class UpdateDbWithNewModelBuilder3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("NotesAppSchema")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FirstWebApi.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Done")
                        .HasColumnType("bit");

                    b.Property<int?>("NotebookId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NotebookId");

                    b.ToTable("Note", "NotesAppSchema");
                });

            modelBuilder.Entity("FirstWebApi.Models.Notebook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoomUniqueKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniqueKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomUniqueKey");

                    b.ToTable("Notebook", "NotesAppSchema");
                });

            modelBuilder.Entity("FirstWebApi.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UniqueKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Room", "NotesAppSchema");
                });

            modelBuilder.Entity("FirstWebApi.Models.Note", b =>
                {
                    b.HasOne("FirstWebApi.Models.Notebook", "Notebook")
                        .WithMany("Notes")
                        .HasForeignKey("NotebookId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Notebook");
                });

            modelBuilder.Entity("FirstWebApi.Models.Notebook", b =>
                {
                    b.HasOne("FirstWebApi.Models.Room", "Room")
                        .WithMany("Notebooks")
                        .HasForeignKey("RoomUniqueKey")
                        .HasPrincipalKey("UniqueKey");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("FirstWebApi.Models.Notebook", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("FirstWebApi.Models.Room", b =>
                {
                    b.Navigation("Notebooks");
                });
#pragma warning restore 612, 618
        }
    }
}

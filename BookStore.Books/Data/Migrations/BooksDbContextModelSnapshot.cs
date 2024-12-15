﻿// <auto-generated />
using System;
using BookStore.Books.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStore.Books.Data.Migrations
{
    [DbContext(typeof(BooksDbContext))]
    partial class BooksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Books")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookStore.Books.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Books", "Books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a89f6cd7-4693-457b-9009-02205dbbfe45"),
                            Author = "J.R.R. Tolkien",
                            Price = 10.99m,
                            Title = "The Fellowship of the Ring"
                        },
                        new
                        {
                            Id = new Guid("e4fa19bf-6981-4e50-a542-7c9b26e9ec31"),
                            Author = "J.R.R. Tolkien",
                            Price = 11.99m,
                            Title = "The Two Towers"
                        },
                        new
                        {
                            Id = new Guid("17c61e41-3953-42cd-8f88-d3f698869b35"),
                            Author = "J.R.R. Tolkien",
                            Price = 12.99m,
                            Title = "The Return of the King"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

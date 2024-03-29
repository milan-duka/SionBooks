﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SionBooks.Data;

namespace SionBooks.Migrations
{
    [DbContext(typeof(SionBooksContext))]
    [Migration("20210711035755_addcomments")]
    partial class addcomments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.BookCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("bookId")
                        .HasColumnType("int");

                    b.Property<string>("text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("bookId");

                    b.HasIndex("userId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.Book", b =>
                {
                    b.HasOne("SionBooks.Areas.Admin.Models.BookCategory", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.Comment", b =>
                {
                    b.HasOne("SionBooks.Areas.Admin.Models.Book", "book")
                        .WithMany("Comments")
                        .HasForeignKey("bookId");

                    b.HasOne("SionBooks.Areas.Admin.Models.User", "user")
                        .WithMany("Comments")
                        .HasForeignKey("userId");

                    b.Navigation("book");

                    b.Navigation("user");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.Vote", b =>
                {
                    b.HasOne("SionBooks.Areas.Admin.Models.Book", "Book")
                        .WithMany("Votes")
                        .HasForeignKey("BookId");

                    b.HasOne("SionBooks.Areas.Admin.Models.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.Book", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.BookCategory", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("SionBooks.Areas.Admin.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}

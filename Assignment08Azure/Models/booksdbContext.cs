using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment08Azure.Models
{
    public partial class booksdbContext : DbContext
    {
        public booksdbContext()
        {
        }

        public booksdbContext(DbContextOptions<booksdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public virtual DbSet<BookCategory> BookCategories { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:booksserver2312.database.windows.net,1433;Initial Catalog=booksdb;Persist Security Info=False;User ID=admin123;Password=admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).ValueGeneratedNever();

                entity.Property(e => e.AuthorName).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).ValueGeneratedNever();

                entity.Property(e => e.Category).HasColumnName("category");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("authid");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("catid");

                entity.HasOne(d => d.PublisherNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Publisher)
                    .HasConstraintName("pubid");
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.ToTable("BookAuthor");

                entity.Property(e => e.BookAuthorId).ValueGeneratedNever();

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("aid");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("bookid");
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__BookCate__23CAF1D88C838610");

                entity.ToTable("BookCategory");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("categoryId");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasColumnName("category");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.PublisherId).ValueGeneratedNever();

                entity.Property(e => e.MobileNumber).HasMaxLength(15);

                entity.Property(e => e.PublisherAddress).HasMaxLength(100);

                entity.Property(e => e.PublisherName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

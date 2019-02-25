using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxelotTestWork.Application.Models
{
    public class DataBaseContext : DbContext
    {
        private readonly string _connString;

        public DataBaseContext() { }

        public DataBaseContext(string connString)
        {
            _connString = connString;
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options) { }

        public static List<T> GetGenericList<T>(string connString) where T : class
        {
            List<T> myDynamicList;

            using (var db = new DataBaseContext(connString))
            {
                // consider using exception handling here as GetDbSet might get an invalid type
                DbSet<T> dbSet = db.GetDbSet<T>();
                myDynamicList = dbSet.Select(p => p).ToList();
            }

            if (myDynamicList != null && myDynamicList.Count() > 0)
            {
                return myDynamicList;
            }
            return new List<T>();
        }

        // in your context class
        public DbSet<T> GetDbSet<T>() where T : class
        {
            return this.Set<T>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connection = $"Data Source={_connString};";
                optionsBuilder.UseSqlite(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Albums>(entity =>
            {
                entity.HasKey(e => e.AlbumId);

                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) entity, "albums");

                entity.HasIndex(e => e.ArtistId)
                    .HasName("IFK_AlbumArtistId");

                entity.Property(e => e.AlbumId).ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    .HasMaxLength(160);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Artists>(entity =>
            {
                entity.HasKey(e => e.ArtistId);

                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) entity, "artists");

                entity.Property(e => e.ArtistId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) entity, "customers");

                entity.HasIndex(e => e.SupportRepId)
                    .HasName("IFK_CustomerSupportRepId");

                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(70);

                entity.Property(e => e.City)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.Property(e => e.Company)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(80);

                entity.Property(e => e.Country)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    .HasMaxLength(60);

                entity.Property(e => e.Fax)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(24);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    .HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(24);

                entity.Property(e => e.PostalCode)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(10);

                entity.Property(e => e.State)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.HasOne(d => d.SupportRep)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.SupportRepId);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) entity, "employees");

                entity.HasIndex(e => e.ReportsTo)
                    .HasName("IFK_EmployeeReportsTo");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(70);

                entity.Property(e => e.City)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.Property(e => e.Country)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.Property(e => e.Email)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(60);

                entity.Property(e => e.Fax)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(24);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    .HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(24);

                entity.Property(e => e.PostalCode)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(10);

                entity.Property(e => e.State)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.Property(e => e.Title)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(30);

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo);
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.GenreId);

                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) entity, "genres");

                entity.Property(e => e.GenreId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<InvoiceItems>(entity =>
            {
                entity.HasKey(e => e.InvoiceLineId);

                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) entity, "invoice_items");

                entity.HasIndex(e => e.InvoiceId)
                    .HasName("IFK_InvoiceLineInvoiceId");

                entity.HasIndex(e => e.TrackId)
                    .HasName("IFK_InvoiceLineTrackId");

                entity.Property(e => e.InvoiceLineId).ValueGeneratedNever();

                entity.Property(e => e.UnitPrice).HasColumnType("numeric(10,2)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.ToTable("invoices");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("IFK_InvoiceCustomerId");

                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.Property(e => e.BillingAddress)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(70);

                entity.Property(e => e.BillingCity)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.Property(e => e.BillingCountry)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.Property(e => e.BillingPostalCode)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(10);

                entity.Property(e => e.BillingState)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40);

                entity.Property(e => e.Total).HasColumnType("numeric(10,2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MediaTypes>(entity =>
            {
                entity.HasKey(e => e.MediaTypeId);

                entity.ToTable("media_types");

                entity.Property(e => e.MediaTypeId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<PlaylistTrack>(entity =>
            {
                entity.HasKey(e => new { e.PlaylistId, e.TrackId });

                entity.ToTable("playlist_track");

                entity.HasIndex(e => e.TrackId)
                    .HasName("IFK_PlaylistTrackTrackId");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.PlaylistTrack)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.PlaylistTrack)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Playlists>(entity =>
            {
                entity.HasKey(e => e.PlaylistId);

                entity.ToTable("playlists");

                entity.Property(e => e.PlaylistId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<Tracks>(entity =>
            {
                entity.HasKey(e => e.TrackId);

                RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) entity, "tracks");

                entity.HasIndex(e => e.AlbumId)
                    .HasName("IFK_TrackAlbumId");

                entity.HasIndex(e => e.GenreId)
                    .HasName("IFK_TrackGenreId");

                entity.HasIndex(e => e.MediaTypeId)
                    .HasName("IFK_TrackMediaTypeId");

                entity.Property(e => e.TrackId).ValueGeneratedNever();

                entity.Property(e => e.Composer)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(220);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    .HasMaxLength(200);

                entity.Property(e => e.UnitPrice).HasColumnType("numeric(10,2)");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.AlbumId);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.GenreId);

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.MediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
  
    }
}

using System;
using System.Collections.Generic;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace SimpleBookingSystem.Models;

public partial class LocalDatabaseContext : DbContext
{
    public LocalDatabaseContext()
    {
    }

    public LocalDatabaseContext(DbContextOptions<LocalDatabaseContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Resource> Resources { get; set; }
    public virtual DbSet<Booking> Bookings { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlite("DataSource=LocalDatabase.db ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<Booking>().HasOne(resource => resource.Resource)
        //       .WithMany()
        //       .HasForeignKey(booking => booking.ResourceId);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}

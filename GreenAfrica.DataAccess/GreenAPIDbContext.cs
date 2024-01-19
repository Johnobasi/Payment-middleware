﻿using GreenAfrica.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenAfrica.DataAccess
{
    public class GreenAPIDbContext : IdentityDbContext
    {
        public GreenAPIDbContext(DbContextOptions<GreenAPIDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CPayroll> Cpayroll { get; set; }
        public virtual DbSet<Booking> Books { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Fleet> Fleets { get; set; }
        public virtual DbSet<TicketBooking> TicketBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<TicketBooking>()
                .HasKey(e => e.Id);

            builder
            .Entity<TicketBooking>()
            .HasOne<FlightInfo>()
            .WithMany(e => e.TicketBookings)
            .HasForeignKey(e => e.FlightId)
            .HasConstraintName("FK_TicketBookingFlightId_FlightInfoFlightId");
        }
    }
}
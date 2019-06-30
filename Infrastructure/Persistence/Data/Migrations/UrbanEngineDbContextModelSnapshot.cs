﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrbanEngine.Infrastructure.Persistence.Data;

namespace UrbanEngine.Infrastructure.Persistence.Data.Migrations
{
    [DbContext(typeof(UrbanEngineDbContext))]
    partial class UrbanEngineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ue")
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("UrbanEngine.Core.Application.Entities.ScheduleAggregate.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("EventType");

                    b.Property<string>("OrganizerId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<long?>("VenueId");

                    b.HasKey("Id");

                    b.HasIndex("VenueId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("UrbanEngine.Core.Application.Entities.ScheduleAggregate.EventVenue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Venue");
                });

            modelBuilder.Entity("UrbanEngine.Core.Application.Entities.ScheduleAggregate.Event", b =>
                {
                    b.HasOne("UrbanEngine.Core.Application.Entities.ScheduleAggregate.EventVenue", "Venue")
                        .WithMany("Events")
                        .HasForeignKey("VenueId");
                });
#pragma warning restore 612, 618
        }
    }
}
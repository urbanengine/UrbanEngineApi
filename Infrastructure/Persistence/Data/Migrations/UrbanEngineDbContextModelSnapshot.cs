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

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("EventType");

                    b.Property<string>("OrganizerId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Event");
                });
#pragma warning restore 612, 618
        }
    }
}

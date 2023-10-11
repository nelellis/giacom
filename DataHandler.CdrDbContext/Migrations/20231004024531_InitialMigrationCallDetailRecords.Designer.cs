﻿// <auto-generated />
using System;
using DataHandler.CdrDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataHandler.CdrDbContext.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231004024531_InitialMigrationCallDetailRecords")]
    partial class InitialMigrationCallDetailRecords
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataHandler.Entities.CallDetailRecord", b =>
                {
                    b.Property<string>("Reference")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("reference");

                    b.Property<DateTime>("CallDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("call_date");

                    b.Property<string>("CallerId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("caller_id");

                    b.Property<decimal>("Cost")
                        .HasPrecision(18, 3)
                        .HasColumnType("decimal(18,3)")
                        .HasColumnName("cost");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency");

                    b.Property<int>("Duration")
                        .HasColumnType("int")
                        .HasColumnName("duration");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time(6)")
                        .HasColumnName("end_time");

                    b.Property<string>("Recipient")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("recipient");

                    b.HasKey("Reference");

                    b.ToTable("call_detail_record", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
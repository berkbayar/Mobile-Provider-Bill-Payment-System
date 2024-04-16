﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobileProviderAPI.Data;

#nullable disable

namespace MobileProviderAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240415072907_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("MobileProviderAPI.Models.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BillMonth")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("TEXT");

                    b.Property<int>("SubscriberId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("TEXT");

                    b.HasKey("BillId");

                    b.HasIndex("SubscriberId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("MobileProviderAPI.Models.Subscriber", b =>
                {
                    b.Property<int>("SubscriberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SubscriberId");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("MobileProviderAPI.Models.Bill", b =>
                {
                    b.HasOne("MobileProviderAPI.Models.Subscriber", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscriber");
                });
#pragma warning restore 612, 618
        }
    }
}

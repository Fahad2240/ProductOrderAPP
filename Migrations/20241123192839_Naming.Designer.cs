﻿// <auto-generated />
using System;
using DotNET.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNET.Migrations
{
    [DbContext(typeof(ProductOrderDbContext))]
    [Migration("20241123192839_Naming")]
    partial class Naming
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DotNET.Models.TblOrders", b =>
                {
                    b.Property<int>("intOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("intOrderId"), 1L, 1);

                    b.Property<int>("IntProductId")
                        .HasColumnType("int");

                    b.Property<int?>("TblProductsIntProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dtOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("numQuantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("strCustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("intOrderId");

                    b.HasIndex("IntProductId");

                    b.HasIndex("TblProductsIntProductId");

                    b.ToTable("tblOrders");
                });

            modelBuilder.Entity("DotNET.Models.TblProducts", b =>
                {
                    b.Property<int>("IntProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IntProductId"), 1L, 1);

                    b.Property<decimal>("numStock")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("numUnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("strProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IntProductId");

                    b.ToTable("tblProducts");
                });

            modelBuilder.Entity("DotNET.Models.TblOrders", b =>
                {
                    b.HasOne("DotNET.Models.TblProducts", "TblProducts")
                        .WithMany()
                        .HasForeignKey("IntProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotNET.Models.TblProducts", null)
                        .WithMany("TblOrders")
                        .HasForeignKey("TblProductsIntProductId");

                    b.Navigation("TblProducts");
                });

            modelBuilder.Entity("DotNET.Models.TblProducts", b =>
                {
                    b.Navigation("TblOrders");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PoolingEngine.DataAccess.Context;

#nullable disable

namespace PoolingEngine.DataAccess.Context.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DeviceItemTagGroup", b =>
                {
                    b.Property<int>("TagGroupsId")
                        .HasColumnType("int");

                    b.Property<int>("deviceItemsId")
                        .HasColumnType("int");

                    b.HasKey("TagGroupsId", "deviceItemsId");

                    b.HasIndex("deviceItemsId");

                    b.ToTable("DeviceItemTagGroup");
                });

            modelBuilder.Entity("PoolingEngine.Domain.Entities.DeviceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("DeviceItems");
                });

            modelBuilder.Entity("PoolingEngine.Domain.Entities.RequestItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DeviceItemId")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("RequestPoolingId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("RequestItem");
                });

            modelBuilder.Entity("PoolingEngine.Domain.Entities.TagDef", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DeviceItemId")
                        .HasColumnType("int");

                    b.Property<string>("MapAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TagItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceItemId");

                    b.HasIndex("TagItemId");

                    b.ToTable("TagDefs");
                });

            modelBuilder.Entity("PoolingEngine.Domain.Entities.TagGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("TagGroups");
                });

            modelBuilder.Entity("PoolingEngine.Domain.Entities.TagItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DataType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TagItems");
                });

            modelBuilder.Entity("PoolingEngine.Domain.Entities.TagValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeviceItemId")
                        .HasColumnType("int");

                    b.Property<string>("RequestItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TagItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("TagValues");
                });

            modelBuilder.Entity("RequestItemTagGroup", b =>
                {
                    b.Property<Guid>("RequestItemsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TagGroupsId")
                        .HasColumnType("int");

                    b.HasKey("RequestItemsId", "TagGroupsId");

                    b.HasIndex("TagGroupsId");

                    b.ToTable("RequestItemTagGroup");
                });

            modelBuilder.Entity("TagGroupTagItem", b =>
                {
                    b.Property<int>("TagGroupsId")
                        .HasColumnType("int");

                    b.Property<int>("TagItemsId")
                        .HasColumnType("int");

                    b.HasKey("TagGroupsId", "TagItemsId");

                    b.HasIndex("TagItemsId");

                    b.ToTable("TagGroupTagItem");
                });

            modelBuilder.Entity("DeviceItemTagGroup", b =>
                {
                    b.HasOne("PoolingEngine.Domain.Entities.TagGroup", null)
                        .WithMany()
                        .HasForeignKey("TagGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoolingEngine.Domain.Entities.DeviceItem", null)
                        .WithMany()
                        .HasForeignKey("deviceItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PoolingEngine.Domain.Entities.TagDef", b =>
                {
                    b.HasOne("PoolingEngine.Domain.Entities.DeviceItem", "DeviceItem")
                        .WithMany("tagDefs")
                        .HasForeignKey("DeviceItemId");

                    b.HasOne("PoolingEngine.Domain.Entities.TagItem", "TagItem")
                        .WithMany("tagDefs")
                        .HasForeignKey("TagItemId");

                    b.Navigation("DeviceItem");

                    b.Navigation("TagItem");
                });

            modelBuilder.Entity("RequestItemTagGroup", b =>
                {
                    b.HasOne("PoolingEngine.Domain.Entities.RequestItem", null)
                        .WithMany()
                        .HasForeignKey("RequestItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoolingEngine.Domain.Entities.TagGroup", null)
                        .WithMany()
                        .HasForeignKey("TagGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TagGroupTagItem", b =>
                {
                    b.HasOne("PoolingEngine.Domain.Entities.TagGroup", null)
                        .WithMany()
                        .HasForeignKey("TagGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoolingEngine.Domain.Entities.TagItem", null)
                        .WithMany()
                        .HasForeignKey("TagItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PoolingEngine.Domain.Entities.DeviceItem", b =>
                {
                    b.Navigation("tagDefs");
                });

            modelBuilder.Entity("PoolingEngine.Domain.Entities.TagItem", b =>
                {
                    b.Navigation("tagDefs");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence;

#nullable disable

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Migrations
{
    [DbContext(typeof(AudioStoreLocalDbContext))]
    partial class AudioStoreLocalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities.AudioListEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CheckForTime")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Size")
                        .HasColumnType("REAL");

                    b.Property<int>("StoreId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("TimeToPlay")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AudioList");
                });

            modelBuilder.Entity("WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities.RegisterEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Activity")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Commited")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("StoreId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("RegisterEntity");
                });

            modelBuilder.Entity("WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("StoreId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Voting.DAL.Context;

namespace Voting.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Voting.DAL.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Voting.DAL.Entities.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("ShowTimes")
                        .HasColumnType("int");

                    b.Property<int>("VotesCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("Voting.DAL.Entities.Pair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FirstModelId")
                        .HasColumnType("int");

                    b.Property<bool>("IsVoted")
                        .HasColumnType("bit");

                    b.Property<int?>("SecondModelId")
                        .HasColumnType("int");

                    b.Property<int>("WinnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FirstModelId");

                    b.HasIndex("SecondModelId");

                    b.ToTable("ModelsPair");
                });

            modelBuilder.Entity("Voting.DAL.Entities.Image", b =>
                {
                    b.HasOne("Voting.DAL.Entities.Model", null)
                        .WithMany("Images")
                        .HasForeignKey("ModelId");
                });

            modelBuilder.Entity("Voting.DAL.Entities.Pair", b =>
                {
                    b.HasOne("Voting.DAL.Entities.Model", "FirstModel")
                        .WithMany()
                        .HasForeignKey("FirstModelId");

                    b.HasOne("Voting.DAL.Entities.Model", "SecondModel")
                        .WithMany()
                        .HasForeignKey("SecondModelId");

                    b.Navigation("FirstModel");

                    b.Navigation("SecondModel");
                });

            modelBuilder.Entity("Voting.DAL.Entities.Model", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}

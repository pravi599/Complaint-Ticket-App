﻿// <auto-generated />
using System;
using ComplaintTicketApp.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ComplaintTicketApp.Migrations
{
    [DbContext(typeof(ComplaintTicketContext))]
    partial class ComplaintTicketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Complaint", b =>
                {
                    b.Property<int>("ComplaintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComplaintId"), 1L, 1);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ComplaintId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("Username");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Analytics", b =>
                {
                    b.Property<int>("AnalyticsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnalyticsId"), 1L, 1);

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("ReportName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnalyticsId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Analytics");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<int>("ComplaintId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommentId");

                    b.HasIndex("ComplaintId");

                    b.HasIndex("Username");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Organization", b =>
                {
                    b.Property<int>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrganizationId"), 1L, 1);

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrganizationId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Priority", b =>
                {
                    b.Property<int>("PriorityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriorityId"), 1L, 1);

                    b.Property<int>("ComplaintId")
                        .HasColumnType("int");

                    b.Property<int>("EscalationThreshold")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PriorityId");

                    b.HasIndex("ComplaintId")
                        .IsUnique();

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Tracking", b =>
                {
                    b.Property<int>("TrackingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackingId"), 1L, 1);

                    b.Property<int>("ComplaintId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TrackingId");

                    b.HasIndex("ComplaintId")
                        .IsUnique();

                    b.ToTable("Trackings");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Key")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Complaint", b =>
                {
                    b.HasOne("ComplaintTicketApp.Models.Organization", "Organization")
                        .WithMany("Complaints")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplaintTicketApp.Models.User", "User")
                        .WithMany("Complaints")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Analytics", b =>
                {
                    b.HasOne("ComplaintTicketApp.Models.Organization", "Organization")
                        .WithMany("AnalyticsReports")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Comment", b =>
                {
                    b.HasOne("Complaint", "Complaint")
                        .WithMany("Comments")
                        .HasForeignKey("ComplaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplaintTicketApp.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Complaint");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Priority", b =>
                {
                    b.HasOne("Complaint", "Complaint")
                        .WithOne("Priority")
                        .HasForeignKey("ComplaintTicketApp.Models.Priority", "ComplaintId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Complaint");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Tracking", b =>
                {
                    b.HasOne("Complaint", "Complaint")
                        .WithOne("Tracking")
                        .HasForeignKey("ComplaintTicketApp.Models.Tracking", "ComplaintId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Complaint");
                });

            modelBuilder.Entity("Complaint", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Priority")
                        .IsRequired();

                    b.Navigation("Tracking")
                        .IsRequired();
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.Organization", b =>
                {
                    b.Navigation("AnalyticsReports");

                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("ComplaintTicketApp.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Complaints");
                });
#pragma warning restore 612, 618
        }
    }
}

using ComplaintTicketApplication.Models;
using ComplaintTicketApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketApplication.Contexts
{
    public class ComplaintTicketContext : DbContext
    {
        public ComplaintTicketContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Analytics> Analytics { get; set; }
        public DbSet<Organization> Organizations { get; set; } // Added Organization DbSet
        public DbSet<Tracking> Trackings { get; set; }
        //public DbSet<ComplaintCategory> ComplaintCategories {  get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Complaint one to many relationship
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.User)
                .WithMany(u => u.Complaints)
                .HasForeignKey(c => c.Username)
                .OnDelete(DeleteBehavior.Restrict);

            // Complaint-Comment one to many relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Complaint)
                .WithMany(complaint => complaint.Comments)
                .HasForeignKey(c => c.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);


            // User-Comment one to many relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.Username)
                .OnDelete(DeleteBehavior.Restrict);

            //
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.AnalyticsReports) // One Organization has many Analytics
                .WithOne(a => a.Organization)      // Each Analytics belongs to one Organization
                .HasForeignKey(a => a.OrganizationId) // Foreign key in Analytics referring to OrganizationId
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete for Analytics when Organization is deleted

            //Organization-Complaint one to many relationship
            modelBuilder.Entity<Complaint>()
            .HasOne(c => c.Organization)
            .WithMany(o => o.Complaints)
            .HasForeignKey(c => c.OrganizationId)
            .IsRequired();

            //Priority-Complaint one to many relationship
            modelBuilder.Entity<Priority>()
            .HasOne(p => p.Complaint)
            .WithOne(c => c.Priority)
            .HasForeignKey<Priority>(p => p.ComplaintId)
            .OnDelete(DeleteBehavior.Restrict);

            //Complaint-Tracking one to many relationship
            modelBuilder.Entity<Tracking>()
            .HasOne(t => t.Complaint)
            .WithOne(c => c.Tracking)
            .HasForeignKey<Tracking>(t => t.ComplaintId)
            .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);


        }
    }
}
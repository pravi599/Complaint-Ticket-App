using ComplaintTicketApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketApp.Contexts
{
    public class ComplaintTicketContext : DbContext
    {
        public ComplaintTicketContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Analytics> Analytics { get; set; }
        public DbSet<Organization> Organizations { get; set; } // Added Organization DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Complaint relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Complaints)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.Username)
                .IsRequired();

            // Complaint-Comment relationship
            modelBuilder.Entity<Complaint>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Complaint)
                .HasForeignKey(c => c.ComplaintId)
                .IsRequired();

            // Complaint-Attachment relationship
            modelBuilder.Entity<Complaint>()
                .HasMany(c => c.Attachments)
                .WithOne(a => a.Complaint)
                .HasForeignKey(a => a.ComplaintId)
                .IsRequired();

            // Priority-Complaint relationship
            modelBuilder.Entity<Priority>()
                .HasMany(p => p.Complaints)
                .WithOne(c => c.Priority)
                .HasForeignKey(c => c.PriorityId)
                .IsRequired();

            // Complaint-ComplaintCategory relationship
            modelBuilder.Entity<ComplaintCategory>()
                .HasMany(cc => cc.Complaints)
                .WithOne(c => c.ComplaintCategory)
                .HasForeignKey(c => c.ComplaintCategoryId)
                .IsRequired(false);

            // Comment-User relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.Username)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);


        }
    }
}

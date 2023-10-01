using Microsoft.EntityFrameworkCore;


namespace ContactsAPI.Models

{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactCategory> ContactCategory { get; set; }
        public DbSet<ContactSubCategory> ContactSubCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }


}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;

namespace Online_Library.Repository
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<User>(options)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<MonthlySubscription> MonthlySubscriptions { get; set; }
        public DbSet<YearlySubscription> YearlySubscriptions { get; set; }

        public DbSet<ReadingList> ReadingLists { get; set; }
        public DbSet<BooksInReadingList> BooksInReadingLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TPH for Subscription
            modelBuilder.Entity<Subscription>()
                .HasDiscriminator<string>("SubscriptionType")
                .HasValue<MonthlySubscription>("Monthly")
                .HasValue<YearlySubscription>("Yearly");
            
            modelBuilder.Entity<User>()
                .HasOne(u => u.ReadingList)
                .WithOne(r => r.User)
                .HasForeignKey<User>(u => u.ReadingListId);
        }
    }
}

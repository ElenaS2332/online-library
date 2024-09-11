using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Online_Library.Domain.Entities;

namespace Online_Library.Repository
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;
        private bool _usePartnerDatabase;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration, bool usePartnerDatabase = false)
            : base(options)
        {
            _configuration = configuration;
            _usePartnerDatabase = usePartnerDatabase;
        }
        
        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<MonthlySubscription> MonthlySubscriptions { get; set; }
        public DbSet<YearlySubscription> YearlySubscriptions { get; set; }

        public DbSet<ReadingList> ReadingLists { get; set; }
        public DbSet<BooksInReadingList> BooksInReadingLists { get; set; }
        public DbSet<Team> Teams { get; set; }
        
        
        public void SwitchToPartherDb(bool setToPartherDb)
        {
            this._usePartnerDatabase = setToPartherDb;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                var connectionString = _usePartnerDatabase
                    ? _configuration.GetConnectionString("PartnerDBConnectionString")
                    : _configuration.GetConnectionString("OnlineLibraryDBConnectionString");

                optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,    
                        maxRetryDelay: TimeSpan.FromSeconds(30), 
                        errorNumbersToAdd: null 
                    );
                });
            }
        }

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
            
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Genre to Books relationship
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
    
}

using Microsoft.EntityFrameworkCore;
using Diplom.Models.Entity;
using Diplom.Models.Account;
using Diplom.Helpers;

namespace Diplom.AppDbContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
               
                builder.ToTable("Users").HasKey(x => x.Id);
                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Name = "admin",
                        Password = HashPasswordHelper.HashPassword("bebra"),
                        Role = Role.Admin
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "testUser",
                        Password = HashPasswordHelper.HashPassword("12345"),
                        Role = Role.User
                    }
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Subscription).WithOne(x => x.User)
                .HasPrincipalKey<User>(x => x.Id).OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(x => x.MyConsultations).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Subscription>(builder =>
            {
                builder.ToTable("Substriptions").HasKey(x => x.Id);
                builder.HasData(
                new Subscription
                {
                    Id = 1,
                    UserId = 1,
                    Consultations = new List<Consultation>()
                },
                new Subscription
                {
                    Id = 2,
                    UserId = 2,
                    Consultations = new List<Consultation>()
                });

                builder.HasMany(x => x.Consultations).WithMany(x => x.Subscriptions)
                       .UsingEntity<Dictionary<string, object>>(
                    "ConsultationSubscription",
                    j => j.HasOne<Consultation>().WithMany().HasForeignKey("ConsultationId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Subscription>().WithMany().HasForeignKey("SubscriptionId").OnDelete(DeleteBehavior.Restrict)
                    );
            });


            modelBuilder.Entity<Consultation>(builder =>
            {
                builder.ToTable("Consultations").HasKey(x => x.Id);
            });


        }


    }
}

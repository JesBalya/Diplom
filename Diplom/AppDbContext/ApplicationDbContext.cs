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

        public DbSet<Group> Groups { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set;}
        public DbSet<User> Users { get; set; }



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
                        Role = Role.Professor
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "testUser",
                        Password = HashPasswordHelper.HashPassword("12345"),
                        Role = Role.Student
                    }
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Student)
                .WithOne(x => x.User)
                .HasPrincipalKey<User>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.Professor)
                .WithOne(x => x.User)
                .HasPrincipalKey<User>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Group>(builder =>
            {
                builder.ToTable("Groups").HasKey(x => x.Id);

                builder.HasMany(x => x.Students).WithOne(x => x.Group).HasForeignKey(x => x.GroupId);
                builder.HasMany(x => x.Consultations).WithOne(x => x.Group).HasForeignKey(x => x.GroupId);

            });

            modelBuilder.Entity<Consultation>(builder =>
            {
                builder.ToTable("Consultations").HasKey(x => x.Id);
            });

            modelBuilder.Entity<Professor>(builder =>
            {
                builder.ToTable("Professors").HasKey(x => x.Id);
                builder.HasMany(x => x.Consultations).WithOne(x => x.Professor).HasForeignKey(x => x.ProfessorId);
            });

            modelBuilder.Entity<Student>(builder =>
            {
                builder.ToTable("Students").HasKey(x => x.Id);
            });

        }


    }
}

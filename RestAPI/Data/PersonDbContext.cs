using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Data
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<PersonInterest> PersonInterests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary key for PersonInterest
            modelBuilder.Entity<PersonInterest>()
                .HasKey(pi => new { pi.PersonId, pi.InterestId });



            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, FirstName = "Ali", LastName = "Derwish", PhoneNumber = "2224444" },
                new Person { Id = 2, FirstName = "Aldor", LastName = "Besher", PhoneNumber = "222333" },
                new Person { Id = 3, FirstName = "Sara", LastName = "Gregor", PhoneNumber = "070675883" }
                );

            modelBuilder.Entity<Interest>().HasData(
                new Interest { Id = 1, Title = "Cooking", Description = "Making food" },
                new Interest { Id = 2, Title = "Drawing", Description = "Painting and drawing" },
                new Interest { Id = 3, Title = "Programming", Description = "Writing code" }
                );

            modelBuilder.Entity<PersonInterest>().HasData(
                new PersonInterest { PersonId = 1, InterestId = 1 },
                new PersonInterest { PersonId = 1, InterestId = 3 },
                new PersonInterest { PersonId = 2, InterestId = 2 },
                new PersonInterest { PersonId = 3, InterestId = 3 }
            );

            modelBuilder.Entity<Link>().HasData(
                new Link { Id = 1, Url = "https://example.com/recipes", Description = "Recipes", PersonId = 1, InterestId = 1 },
                new Link { Id = 2, Url = "https://example.com/csharp", Description = "C# tutorials", PersonId = 1, InterestId = 3 },
                new Link { Id = 3, Url = "https://example.com/art", Description = "Art inspiration", PersonId = 2, InterestId = 2 }
            );
        }
    }
}

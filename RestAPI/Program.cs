
using Microsoft.EntityFrameworkCore;
using RestAPI.Data;
using RestAPI.Models;

namespace RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<PersonDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Get all persons
            app.MapGet("/persons", (PersonDbContext context) =>
            {
                var persons = context.Persons.ToList();

                if (!persons.Any()) // Check if the list is empty
                {
                    return Results.NotFound("No persons found.");
                }

                return Results.Ok(persons);
            });

            // Hämta alla intressen som är kopplade till en specifik person
            app.MapGet("/persons/{id}/interests", (PersonDbContext context, int id) =>

            {
                var interests = context.PersonInterests
                .Where(p => p.PersonId == id)
                .Select(p => p.Interest)
                .ToList();



                if (!interests.Any()) // Check if the list is empty
                {
                    return Results.NotFound("No interests found for this person.");
                }

                return Results.Ok(interests); 

            });

            // Hämta alla länkar som är kopplade till en specifik person
            app.MapGet("/persons/{id}/links", (PersonDbContext context, int id) =>

            {
                var links = context.Links
                .Where(p => p.PersonId == id)
                .Select(p => p)
                .ToList();



                if (!links.Any()) // Check if the list is empty
                {
                    return Results.NotFound("No links found for this person.");
                }

                return Results.Ok(links);

            });


            //Koppla en person till ett nytt intresse
            app.MapPut("/persons/{personId}/interests/{interestId}", (PersonDbContext context, int personId, int interestId) =>
            {

                var personInterest = context.Persons
                .Where(p => p.Id == personId)
                .ToList();

                if (!personInterest.Any()) // Check if the list is empty
                {
                    return Results.NotFound($"No person with {personId} found");
                }


                var interest = context.Interests
                .Where(i => i.Id == interestId)
                .ToList();

                if (!interest.Any()) // Check if the list is empty
                {
                    return Results.NotFound($"No interest with {interestId} found");
                }

                // Prevent duplicate relationship
                if (context.PersonInterests.Any(pi =>
                    pi.PersonId == personId && pi.InterestId == interestId))
                    return Results.Conflict("Person already has this interest");


                context.PersonInterests.Add(new PersonInterest
                {
                    PersonId = personId,
                    InterestId = interestId
                });

                context.SaveChanges();
           
                return Results.Ok("Interest linked to person");
            });

            // Lägga in nya länkar för en specifik person och ett specifikt intresse
            app.MapPost("/persons/{personId}/interests/{interestId}/links", (PersonDbContext context, int personId, int interestId, Link newLink) =>
            {
                var person = context.Persons
                .Where(p => p.Id == personId)
                .ToList();

                if (!person.Any()) // Check if the list is empty
                {
                    return Results.NotFound($"No person with {personId} found");
                }


                var interest = context.Interests
                .Where(i => i.Id == interestId)
                .ToList();

                if (!interest.Any()) // Check if the list is empty
                {
                    return Results.NotFound($"No interest with {interestId} found");
                }

                var link = new Link
                {
                    Url = newLink.Url,
                    Description = newLink.Description,
                    PersonId = personId,
                    InterestId = interestId
                };

                context.Links.Add(link);
                context.SaveChanges();

                return Results.Created($"/persons/{personId}/interests/{interestId}/links/{link.Id}", link); // Return the created link with its new ID
            });



            app.Run();
        }
    }
}

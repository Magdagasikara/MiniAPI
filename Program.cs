using Microsoft.EntityFrameworkCore;
using MiniAPI.Data;
using MiniAPI.Handlers;
using MiniAPI.Models;

namespace MiniAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            var app = builder.Build();


            //GETS
            app.MapGet("/", (ApplicationContext context) => { return Results.Text("Hello! Try sth else!"); });


            //get all personer in the system
            app.MapGet("/person", PersonHandler.ListPersons);

            //get all interests for chosen person
            app.MapGet("/person/{personId}/interest", PersonHandler.ListPersonsInterests);

            //get all links for chosen person
            app.MapGet("/person/{personId}/link", LinkHandler.ListPersonsLinks);

            //get all interests and links for chosen person in a hierarchical json
            app.MapGet("/person/{personId}/interest/link", LinkHandler.ListPersonsInterestsAndLinks);

            //POSTS
            //connect a person with a new interest
            app.MapPost("/person/{personId}/interest/{interestId}", InterestHandler.AddPersonsInterest);
            //add new links for specific interest of a specific person 
            app.MapPost("/person/{personId}/interest/{interestId}/link", InterestHandler.CreatePersonsLinks);

            
            // just to help me create db:
            app.MapPost("/person", PersonHandler.CreatePersons);
            app.MapPost("/interest", InterestHandler.CreateInterests);
            app.MapPost("/person/{personId}/interest", InterestHandler.AddPersonsInterests);


            app.Run();
        }
    }
}

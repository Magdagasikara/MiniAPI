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
            //  OBS Skapa paginering av anropen. När jag anropar exempelvis personer får jag kanske de första 100 personerna och får sen anropa ytterligare gånger för att få fler. Här kan det också vara snyggt att anropet avgör hur många personer jag får i ett anrop så jag kan välja att få säg 10st om jag bara vill ha det.
            //har jag annat alternativ att fånga användarens input i insomnia än sökvägen?
            app.MapGet("/person/list/{options}", PersonHandler.ListPersons);

            //get all interests for chosen person
            //  OBS (either ID or first letters of the name etc)
            app.MapGet("/person/{personId}/interest", PersonHandler.ListPersonsInterests);

            //get all links for chosen person
            //  OBS (either ID or first letters of the name etc)
            app.MapGet("/person/{personId}/link", LinkHandler.ListPersonsLinks);

            //get all interests and links for chosen person in a hierarchical json
            //  OBS (either ID or first letters of the name etc)
            //länken måste skilja nåt från ovan
            app.MapGet("/person/{personId}/interest/link", LinkHandler.ListPersonsInterestsAndLinks);

            //POSTS
            //connect a person with a new interest
            app.MapPost("/person/{personId}/interest/{interestId}", InterestHandler.AddPersonsInterest);
            //  OBS maybe start typing interest and if only one like that this will be the one to add
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

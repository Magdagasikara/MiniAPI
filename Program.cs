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

            app.MapGet("/", () => "Hello World!");

            //GETS
            //get all personer in the system
            //  OBS Skapa paginering av anropen. N�r jag anropar exempelvis personer f�r jag kanske de f�rsta 100 personerna och f�r sen anropa ytterligare g�nger f�r att f� fler. H�r kan det ocks� vara snyggt att anropet avg�r hur m�nga personer jag f�r i ett anrop s� jag kan v�lja att f� s�g 10st om jag bara vill ha det.
            //har jag annat alternativ att f�nga anv�ndarens input i insomnia �n s�kv�gen?
            app.MapGet("/person/{options}", PersonHandler.ListPersons);

            //get all interests for chosen person
            //  OBS (either ID or first letters of the name etc)
            //get all links for chosen person
            //  OBS (either ID or first letters of the name etc)
            //get all interests and links for chosen person in a hierarchical json
            //  OBS (either ID or first letters of the name etc)

            //POSTS
            //connect a person with a new interest
            //  OBS maybe start typing interest and if only one like that this will be the one to add
            //add new links for specific interest of a specific person 
            app.MapPost("/", (ApplicationContext context) =>{});

            // just to help me create db:
            app.MapPost("/", CreatePerson);

            app.Run();
        }
    }
}

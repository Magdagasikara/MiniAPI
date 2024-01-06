using Microsoft.EntityFrameworkCore;
using MiniAPI.Data;
using MiniAPI.Models;
using MiniAPI.Models.Dtos;
using MiniAPI.Models.ViewModels;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MiniAPI.Handlers
{
    public static class PersonHandler
    {

        public static int pageNumber = 0; //ökar med användning av ListPersons

        /// <summary>
        /// Everything at once!
        /// List persons in DB - either all of them or selected with filters
        /// </summary>
        /// <param name="options">Enter a number to see that amount of persons per page. Enter a string to show persons with names starting with it. </param>
        public static IResult ListPersons(ApplicationContext context, string? options)
        {

            ListPersonsViewModel[] result = context.Persons
                .Select(p => new ListPersonsViewModel()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PhoneNumber = p.PhoneNumber,
                })
                .ToArray();


            // Show all persons
            if (options is null || options == "0" || options.ToUpper() == "ALL")
            {
                return Results.Json(result);
            }


            // Show a filtered list
            int amountPerPage;
            if (!int.TryParse(options, out amountPerPage))
            {
                ListPersonsViewModel[] resultFilter = result
                    .Where(n => n.FirstName.ToUpper().StartsWith(options.ToUpper()))
                    .ToArray();

                return Results.Json(resultFilter);
            }


            // Show all persons on several pages 
            int numberOfPages = (int)Math.Ceiling((decimal)result.Length / amountPerPage);
            int amountOnThisPage = pageNumber < numberOfPages - 1 ? amountPerPage : result.Length % amountPerPage;
            pageNumber = pageNumber >= numberOfPages - 1 ? 0 : pageNumber++;

            ListPersonsViewModel[] resultPart = result
                .Skip(numberOfPages * amountPerPage)
                .Take(amountOnThisPage)
                .ToArray();

            return Results.Json(resultPart);

        }
        public static IResult ListPersonsInterests(ApplicationContext context, int personId)
        {
            var person = context.Persons
                .Include(p => p.Interests)
                .SingleOrDefault(p => p.Id == personId);

            if (person is null)
            {
                return Results.NotFound($"person {personId} not found");
            }

            ListPersonsInterestsViewModel[] result = person
                .Interests
                .Select(p => new ListPersonsInterestsViewModel()
                {
                    Title = p.Title,
                    Description = p.Description,
                })
                .ToArray();

            return Results.Json(result);
        }
        public static IResult CreatePersons(ApplicationContext context, PersonDto[] persons)
        {
            foreach (PersonDto person in persons)
            {
                context.Persons.Add(new Person()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    PhoneNumber = person.PhoneNumber
                });
            }
            try
            {
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch
            {
                Console.WriteLine("Error saving new person");
                return Results.BadRequest();
            }
        }
    }
}

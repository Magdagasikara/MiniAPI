using Microsoft.EntityFrameworkCore;
using MiniAPI.Data;
using MiniAPI.Models;
using MiniAPI.Models.Dtos;
using MiniAPI.Models.ViewModels;
using System.Net;

namespace MiniAPI.Handlers
{
    public static class PersonHandler
    {


        public static IResult ListPersons(ApplicationContext context, string? firstName, string? lastName, int? amountPerPage, int? pageNumber)
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
            if (firstName is null && lastName is null && amountPerPage is null && pageNumber is null)
            {
                return Results.Json(result);
            }


            // Show a filtered list       
            if (firstName is not null)
            {
                result = result
                        .Where(n => n.FirstName.ToUpper().StartsWith(firstName.ToUpper()))
                        .ToArray();
            }
            if (lastName is not null)
            {
                result = result
                        .Where(n => n.LastName.ToUpper().StartsWith(lastName.ToUpper()))
                        .ToArray();
            }


            // Show subpages
            // check if amountPerPage & pageNumber are integers, otherwise return bad request
            if (amountPerPage is not null || pageNumber is not null)
            {
                int parsedAmountPerPage = 10; //sets default value if only pageNumber not null
                if (amountPerPage is not null && !int.TryParse(amountPerPage.ToString(), out parsedAmountPerPage))
                {
                    return Results.BadRequest("amountPerPage måste vara en integer");
                }
                int parsedPageNumber = 1; //sets default value if only amountPerPage not null
                if (pageNumber is not null && !int.TryParse(pageNumber.ToString(), out parsedPageNumber))
                {
                    return Results.BadRequest("pageNumber måste vara en integer");
                }

                int skipAmount = parsedAmountPerPage * (parsedPageNumber-1);
                int numberOfPages = (int)Math.Ceiling((decimal)result.Length / parsedAmountPerPage);
                int amountOnThisPage = parsedPageNumber < numberOfPages ? parsedAmountPerPage : result.Length % parsedAmountPerPage;

                result = result
                 .Skip(skipAmount)
                 .Take(amountOnThisPage)
                 .ToArray();

            }

            return Results.Json(result);
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

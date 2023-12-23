using MiniAPI.Data;
using MiniAPI.Models;
using MiniAPI.Models.Dtos;
using MiniAPI.Models.ViewModels;

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
        public static IResult ListPersons(ApplicationContext context, string options = "")
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
            if (options is null || options == "0")
            {
                return Results.Json(result);
            }


            // Show a filtered list
            int amountPerPage;
            if (!int.TryParse(options, out amountPerPage))
            {
                ListPersonsViewModel[] resultFilter = result
                    .Where(n => n.FirstName.StartsWith(options))
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

        public static IResult CreatePerson(ApplicationContext context, PersonDto person)
        {
            context.Persons.Add(new Person()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
            });
            context.SaveChanges();

            // kontrollera om det gick bra innan jag skickar tillbaka ok

        }
    }
}

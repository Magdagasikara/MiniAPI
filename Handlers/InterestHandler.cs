
using Microsoft.EntityFrameworkCore;
using MiniAPI.Data;
using MiniAPI.Models;
using MiniAPI.Models.Dtos;
using MiniAPI.Models.ViewModels;
using System.Net;
namespace MiniAPI.Handlers
{
    public static class InterestHandler
    {

        public static IResult CreateInterests(ApplicationContext context, InterestDto[] interests)
        {
            foreach (InterestDto interest in interests)
            {
                context.Interests
                    .Add(new Interest()
                    {
                        Title = interest.Title,
                        Description = interest.Description,
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
                return Results.BadRequest(); // jag vet inte bättre
            }
        }

        public static IResult AddPersonsInterest(ApplicationContext context, int personId, int interestId)
        {
            Person? person = context.Persons
                .Where(p => p.Id == personId)
                .Include(p => p.Interests)
                .SingleOrDefault();

            if (person is null)
            {
                return Results.NotFound($"person {personId} not found");
            }

            Interest? interest = context.Interests
                    .SingleOrDefault(i => i.Id == interestId);

            if (interest is null)
            {
                return Results.NotFound($"interest {interestId} not found");
            }

            person.Interests
                      .Add(interest);

            try
            {
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch
            {
                // jag vill skriva ut vilken interest saknas om det är det som är problemet
                // $"Error saving new interests of person with id {personId}");
                return Results.BadRequest(); // jag vet inte bättre
            }
        }
        public static IResult AddPersonsInterests(ApplicationContext context, int personId, /*int[] interestIds */InterestIdDto[] interestIds)
        {
            Person? person = context.Persons
                .Where(p => p.Id == personId)
                .Include(p => p.Interests)
                .SingleOrDefault();

            if (person is null)
            {
                return Results.NotFound($"person {personId} not found");
            }

            //borde jag kolla om personen redan har intresset eller sköter det sig själv?
            foreach (InterestIdDto interestId in interestIds)
            {
                Interest? interest = context.Interests
                    .SingleOrDefault(i => i.Id == interestId.Id);

                if (interest is null)
                {
                    return Results.NotFound($"interest {interestId} not found");
                }

                if (!person.Interests.Any(i => i.Id == interestId.Id))
                {
                    person.Interests.Add(interest);
                }
                else
                    return Results.BadRequest($"at least one of interests already exists");

                // instansierar InterestLinks direkt
                //person.InterestLinks = new List<InterestLink>();

            }
            try
            {
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            //catch
            //{
            //    // jag vill skriva ut vilken interest saknas om det är det som är problemet
            //    // $"Error saving new interests of person with id {personId}");
            //    return Results.BadRequest(); // jag vet inte bättre
            //}
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                return Results.BadRequest();
            }
        }

        public static IResult CreatePersonsLinks(ApplicationContext context, int personId, int interestId, InterestLinkDto[] interestLinks)
        {
            Person? person = context.Persons
                .Include(p => p.Interests)
                .Include(p => p.InterestLinks)
                .SingleOrDefault(p => p.Id == personId);

            if (person is null)
            {
                return Results.NotFound($"person {personId} not found");
            }

            Interest? interest = person.Interests
                .SingleOrDefault(i => i.Id == interestId);


            if (interest is null)
            {
                return Results.NotFound($"interest {interestId} not found for person {personId} ");
            }

            // OBS fundera lite till på varför jag ska göra det här
            //if (interest.InterestLinks is null)
            //{
            //    interest.InterestLinks = new List<InterestLink>();
            //}

            foreach (InterestLinkDto interestLink in interestLinks)
            {
                interest.InterestLinks
                          .Add(new InterestLink()
                          {
                              Link = interestLink.Link,
                              Person = person,
                              Interest = interest
                          });
            }
            try
            {
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch
            {
                Console.WriteLine($"Error saving new links of person with id {personId} and interest with id {interestId}");
                return Results.BadRequest(); // jag vet inte bättre
            }

        }
    }
}

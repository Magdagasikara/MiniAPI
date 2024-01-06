using Microsoft.EntityFrameworkCore;
using MiniAPI.Data;
using MiniAPI.Models.Dtos;
using MiniAPI.Models.ViewModels;

namespace MiniAPI.Handlers
{
    public static class LinkHandler
    {
        // nu vet jag egentligen inte hur jag ska tänka med LinkHandler, InterestHandler etc
        // jag gör så att jag väjer den lägsta nivån så finns länkar med så lägger jag här
        // skulle vi ha som uppgift att lista alla personer och intressen så vet jag inte vad jag skulle göra för de är på samma nivå
        // <bajs>

        public static IResult ListPersonsLinks(ApplicationContext context, int personId)
        {
            var person = context.Persons
                .Include(p => p.InterestLinks)
                .SingleOrDefault(p => p.Id == personId);

            if (person == null)
            {
                return Results.NotFound($"person {personId} not found");
            }

            ListPersonsLinksViewModel[] result=person
                .InterestLinks
                .Select(p => new ListPersonsLinksViewModel()
                {
                    Link=p.Link
                })
                .ToArray();

            return Results.Json(result);

        }

        public static IResult ListPersonsInterestsAndLinks(ApplicationContext context, int personId)
        {

            var person = context.Persons
                .Include(p => p.Interests)
                .Include(p => p.InterestLinks)
                .SingleOrDefault(p=> p.Id == personId);

            if (person is null)
            {
                return Results.NotFound($"person {personId} not found");
            }

            ListPersonsInterestsAndLinksViewModel[] result=person.Interests
                .Select(p => new ListPersonsInterestsAndLinksViewModel()
                {
                    Title = p.Title,
                    Description = p.Description,
                    InterestLinks = p.InterestLinks
                    .Select(i => new ListLinksViewModel() { Link = i.Link })
                    .ToArray()
                })
                .ToArray();
                
            return Results.Json(result);
        }
    }
}

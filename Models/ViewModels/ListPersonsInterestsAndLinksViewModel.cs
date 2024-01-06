using MiniAPI.Models.Dtos;

namespace MiniAPI.Models.ViewModels
{
    public class ListPersonsInterestsAndLinksViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<ListLinksViewModel> InterestLinks { get; set; }
    }
    public class ListLinksViewModel
    {
        public string Link { get; set; }
    }
}

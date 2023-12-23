namespace MiniAPI.Models.ViewModels
{
    public class ListPersonsViewModel
    {

        // kan jag lägga till antalet personer som finns med totalt på nåt sätt?
        public int Id { get; set; } // in the future we will separate Id here from the one in Models, somehow
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}

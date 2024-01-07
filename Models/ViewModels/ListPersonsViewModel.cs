namespace MiniAPI.Models.ViewModels
{
    public class ListPersonsViewModel
    {

        public int Id { get; set; } // in the future we will separate Id here from the one in Models, somehow
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}

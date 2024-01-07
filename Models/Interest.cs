namespace MiniAPI.Models
{
    public class Interest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Person> Persons{ get; set; }
        public virtual ICollection<InterestLink> InterestLinks{ get; set; }

        // fundera på det 
        //public Interest()
        //{
        //    InterestLinks = new List<InterestLink>(); 
        //}
    }
}

namespace RestAPI.Models
{
    public class Link
    {
        public int Id { get; set; } 
        public string Url { get; set; }
        public string Description { get; set; }

        public int PersonId { get; set; }
        public int InterestId { get; set; } 

    }
}

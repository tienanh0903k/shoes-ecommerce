namespace ShoesStoreApp.DAL.Models
{
    public class Blog
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string BlogImage { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

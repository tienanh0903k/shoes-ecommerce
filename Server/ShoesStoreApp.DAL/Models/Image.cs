namespace ShoesStoreApp.DAL.Models
{
    public  class ImageSystem
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

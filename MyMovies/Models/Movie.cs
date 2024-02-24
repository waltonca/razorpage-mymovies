namespace MyMovies.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Genre { get; set; } = String.Empty;
        public int Year { get; set; }
        public DateTime PublishDate { get; set; }
        public string FileName { get; set; } = string.Empty;
        public Category Category { get; set; } = new();
    }
}

namespace Movies.Api.Dtos
{
    public class MovieDto
    {
        public string? Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(250)]
        public string? StoryLine { get; set; }
        public IFormFile? Poster { get; set; }
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
    }
}

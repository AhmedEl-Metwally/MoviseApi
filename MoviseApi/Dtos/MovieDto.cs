namespace MoviseApi.Dtos
{
    public class MovieDto
    {
        [MaxLength(100)]
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Ratr { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        [Required]
        public IFormFile? Poster { get; set; }
        public byte GenreId { get; set; }
    }
}

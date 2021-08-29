namespace Entities.Dtos
{
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public int GenreId { get; set; }
        public int DirectorId { get; set; }
    }
}
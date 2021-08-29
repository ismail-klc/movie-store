using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GenreMovieViewModel> Movies { get; set; }
    }

    public class GenreMovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
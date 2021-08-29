using System.Collections.Generic;

namespace Entities.ViewModels
{
    
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public MovieGenreViewModel Genre { get; set; }
        public MovieDirectorViewModel Director { get; set; }
        public ICollection<MovieActorViewModel> Actors { get; set; }
    }

    public class MovieGenreViewModel
    {
         public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MovieDirectorViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public class MovieActorViewModel : MovieDirectorViewModel
    {
    }
}
using System.Collections.Generic;

namespace Entities.ViewModels
{
    
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public MovieGenreResponse Genre { get; set; }
        public MovieDirectorReponse Director { get; set; }
        public ICollection<MovieActorReponse> Actors { get; set; }
    }

    public class MovieGenreResponse
    {
         public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MovieDirectorReponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public class MovieActorReponse : MovieDirectorReponse
    {
    }
}
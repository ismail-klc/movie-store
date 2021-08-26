using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class ActorResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<ActorMovieResponse> Movies { get; set; }
    }

    public class ActorMovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
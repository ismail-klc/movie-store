using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class ActorViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<ActorMovieViewModel> Movies { get; set; }
    }

    public class ActorMovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
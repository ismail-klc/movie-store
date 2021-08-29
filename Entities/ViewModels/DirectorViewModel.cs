using System.Collections.Generic;

namespace Entities.ViewModels
{
   public class DirectorViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<DirectorMovieViewModel> Movies { get; set; }
    }

    public class DirectorMovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
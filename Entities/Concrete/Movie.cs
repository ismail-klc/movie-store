using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public Genre Genre { get; set; }
        public Director Director { get; set; }
        public ICollection<Actor> Actors { get; set; }
    }
}

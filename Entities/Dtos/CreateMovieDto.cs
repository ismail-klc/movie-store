using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CreateMovieDto
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public int GenreId { get; set; }
        public int DirectorId { get; set; }
    }
}

using Entities.Concrete;
using Entities.Dtos;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMovieService
    {
        Task AddMovie(CreateMovieDto movieDto);
        Task<List<MovieViewModel>> GetMovies();
    }
}

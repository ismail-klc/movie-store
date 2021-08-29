using Business.Abstract;
using Data.Concrete;
using AutoMapper;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Business.Exceptions;
using Entities.ViewModels;

namespace Business.Concrete
{
    public class MovieService : IMovieService
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public MovieService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddMovie(CreateMovieDto movieDto)
        {
            var directorExisted = await _context.Directors.AnyAsync(x => x.Id == movieDto.DirectorId);
            if (!directorExisted)
            {
                throw new BadRequestException("Director not found");
            }

            var genreExisted = await _context.Genres.AnyAsync(x => x.Id == movieDto.GenreId);
            if (!genreExisted)
            {
                throw new BadRequestException("Genre not found");
            }

            var movie = _mapper.Map<Movie>(movieDto);
            var addedMovie = _context.Entry(movie);
            addedMovie.State = EntityState.Added;
            
            await _context.SaveChangesAsync();
        }

        public async Task<List<MovieViewModel>> GetMovies()
        {
            var movies = await _context.Movies
                .Include(x => x.Director).Include(x => x.Actors)
                .Include(x => x.Genre).ToListAsync();
            
            return _mapper.Map<List<MovieViewModel>>(movies);
        }
    }
}

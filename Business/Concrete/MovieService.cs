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

        public async Task<MovieViewModel> GetMovieById(int id)
        {
            var movie = await _context.Set<Movie>()
                .Include(x => x.Director).Include(x => x.Actors)
                .Include(x => x.Genre).FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null)
            {
                throw new NotFoundException("Movie not found");
            }

            return _mapper.Map<MovieViewModel>(movie);
        }

        public async Task<List<MovieViewModel>> GetMovies()
        {
            var movies = await _context.Movies
                .Include(x => x.Director).Include(x => x.Actors)
                .Include(x => x.Genre).ToListAsync();

            return _mapper.Map<List<MovieViewModel>>(movies);
        }

        public async Task UpdateMovie(UpdateMovieDto movieDto)
        {
            try
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
                movie.Director = await _context.Directors.FirstOrDefaultAsync(x => x.Id == movieDto.DirectorId);
                movie.Genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == movieDto.GenreId);
                var updatedMovie = _context.Entry(movie);
                updatedMovie.State = EntityState.Modified;

                await _context.SaveChangesAsync(); 
            }
            catch (System.Exception ex)
            {
                throw new BadRequestException("Movie not updated");
            }
        }
    }
}

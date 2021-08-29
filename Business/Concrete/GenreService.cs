using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Exceptions;
using Data.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class GenreService : IGenreService
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public GenreService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddGenre(CreateGenreDto dto)
        {
            var genre = _mapper.Map<Genre>(dto);
            var addedGenre = _context.Entry(genre);
            addedGenre.State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task<GenreViewModel> GetGenreById(int id)
        {
            var genre = await _context.Set<Genre>()
                .Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
            {
                throw new NotFoundException("Genre not found");
            }

            return _mapper.Map<GenreViewModel>(genre);
        }

        public async Task<List<GenreViewModel>> GetGenres()
        {
            var genres = await _context.Genres.Include(x => x.Movies).ToListAsync();
            return _mapper.Map<List<GenreViewModel>>(genres);
        }

        public async Task UpdateGenre(UpdateGenreDto dto)
        {
            try
            {
                var genre = _mapper.Map<Genre>(dto);
                var updatedGenre = _context.Entry(genre);
                updatedGenre.State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new BadRequestException("Director not updated");
            }
        }
    }
}
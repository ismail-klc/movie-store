using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Data.Concrete;
using Entities.Concrete;
using Entities.Dtos;
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

        public async Task<List<Genre>> GetGenres()
        {
            var genres = await _context.Genres.Include(x => x.Movies).ToListAsync();
            return genres;
        }
    }
}
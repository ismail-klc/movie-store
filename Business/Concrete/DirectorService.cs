using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Validations;
using Data.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class DirectorService : IDirectorService
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public DirectorService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddDirector(CreateDirectorDto directorDto)
        {
            var director = _mapper.Map<Director>(directorDto);
            var addedDirector = _context.Entry(director);
            addedDirector.State = EntityState.Added;
            
            await _context.SaveChangesAsync();
        }

        public async Task<List<Director>> GetDirectors()
        {
            var directors = await _context.Directors.Include(x => x.Movies).ToListAsync();
            return directors;
        }
    }
}
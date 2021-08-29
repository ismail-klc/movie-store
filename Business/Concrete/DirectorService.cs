using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Exceptions;
using Business.Validations;
using Data.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Entities.ViewModels;
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

        public async Task<DirectorViewModel> GetDirectorById(int id)
        {
            var director = await _context.Set<Director>()
                .Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            if (director == null)
            {
                throw new NotFoundException("Director not found");
            }

            return _mapper.Map<DirectorViewModel>(director);
        }

        public async Task<List<DirectorViewModel>> GetDirectors()
        {
            var directors = await _context.Directors.Include(x => x.Movies).ToListAsync();
            return _mapper.Map<List<DirectorViewModel>>(directors);
        }

        public async Task UpdateDirector(UpdateDirectorDto dto)
        {
            try
            {
                var director = _mapper.Map<Director>(dto);
                var updatedDirector = _context.Entry(director);
                updatedDirector.State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new BadRequestException("Director not updated");
            }
        }
    }
}
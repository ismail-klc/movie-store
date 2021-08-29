using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Data.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using Business.Exceptions;
using Entities.ViewModels;

namespace Business.Concrete
{
    public class ActorService : IActorService
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public ActorService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddActor(CreateActorDto dto)
        {
            var actor = _mapper.Map<Actor>(dto);
            var addedActor = _context.Entry(actor);
            addedActor.State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task AddMovieToActor(int actorId, int movieId)
        {
            // check if actor exists
            var actor = await _context.Set<Actor>().Include(x => x.Movies).SingleOrDefaultAsync(x => x.Id == actorId);
            if (actor == null)
            {
                throw new BadRequestException("Actor not found");
            }

            // check if movie exists
            var movie = await _context.Set<Movie>().SingleOrDefaultAsync(x => x.Id == movieId);
            if (movie == null)
            {
                throw new BadRequestException("Movie not found");
            }

            // check if actor has movie
            if (actor.Movies.Contains(movie))
            {
                throw new BadRequestException("The actor has already got this movie");
            }

            // add movie to actor
            actor.Movies.Add(movie);
            var updatedActor = _context.Entry(actor);
            updatedActor.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<ActorViewModel> GetActorById(int id)
        {
            var actor = await _context.Set<Actor>()
                .Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null)
            {
                throw new NotFoundException("Actor not found");
            }

            return _mapper.Map<ActorViewModel>(actor);
        }

        public async Task<List<ActorViewModel>> GetActors()
        {
            var actors = await _context.Set<Actor>()
                .Include(x => x.Movies).ToListAsync();

            return _mapper.Map<List<ActorViewModel>>(actors);
        }
    }
}
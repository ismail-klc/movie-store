using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;
using Entities.ViewModels;

namespace Business.Abstract
{
    public interface IActorService
    {
        Task AddActor(CreateActorDto dto);
        Task<List<ActorResponse>> GetActors();
        Task AddMovieToActor(int actorId, int movieId);
    }
}
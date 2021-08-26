using AutoMapper;
using Entities.Dtos;
using Entities.Concrete;
using Entities.ViewModels;

namespace Business.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Admin, CreateAdminDto>().ReverseMap();

            CreateMap<Movie, CreateMovieDto>().ReverseMap();
            CreateMap<Movie, ActorMovieResponse>();
            CreateMap<Movie, MovieResponse>();

            CreateMap<Director, CreateDirectorDto>().ReverseMap();
            CreateMap<Director, MovieDirectorReponse>();

            CreateMap<Actor, CreateActorDto>().ReverseMap();
            CreateMap<Actor, MovieActorReponse>();
            CreateMap<Actor, ActorResponse>();

            CreateMap<Genre, CreateGenreDto>().ReverseMap();
            CreateMap<Genre, MovieGenreResponse>();
        }
    }
}
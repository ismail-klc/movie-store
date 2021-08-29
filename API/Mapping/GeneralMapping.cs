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

            CreateMap<Customer, CreateCustomerDto>().ReverseMap();

            CreateMap<Movie, CreateMovieDto>().ReverseMap();
            CreateMap<Movie, ActorMovieViewModel>();
            CreateMap<Movie, DirectorMovieViewModel>();
            CreateMap<Movie, MovieViewModel>();

            CreateMap<Director, CreateDirectorDto>().ReverseMap();
            CreateMap<Director, MovieDirectorViewModel>();
            CreateMap<Director, DirectorViewModel>();

            CreateMap<Actor, CreateActorDto>().ReverseMap();
            CreateMap<Actor, MovieActorViewModel>();
            CreateMap<Actor, ActorViewModel>();

            CreateMap<Genre, CreateGenreDto>().ReverseMap();
            CreateMap<Genre, MovieGenreViewModel>();
        }
    }
}
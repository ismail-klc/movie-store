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
            CreateMap<Movie, UpdateMovieDto>().ReverseMap();
            CreateMap<Movie, ActorMovieViewModel>();
            CreateMap<Movie, DirectorMovieViewModel>();
            CreateMap<Movie, MovieViewModel>();
            CreateMap<Movie, GenreMovieViewModel>();

            CreateMap<Director, CreateDirectorDto>().ReverseMap();
            CreateMap<Director, UpdateDirectorDto>().ReverseMap();
            CreateMap<Director, MovieDirectorViewModel>();
            CreateMap<Director, DirectorViewModel>();

            CreateMap<Actor, CreateActorDto>().ReverseMap();
            CreateMap<Actor, UpdateActorDto>().ReverseMap();
            CreateMap<Actor, MovieActorViewModel>();
            CreateMap<Actor, ActorViewModel>();

            CreateMap<Genre, CreateGenreDto>().ReverseMap();
            CreateMap<Genre, UpdateGenreDto>().ReverseMap();
            CreateMap<Genre, MovieGenreViewModel>();
            CreateMap<Genre, GenreViewModel>();
        }
    }
}
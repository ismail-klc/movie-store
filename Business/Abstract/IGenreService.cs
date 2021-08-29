using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;
using Entities.ViewModels;

namespace Business.Abstract
{
    public interface IGenreService
    {
         Task AddGenre(CreateGenreDto dto);
        Task<List<GenreViewModel>> GetGenres();
    }
}
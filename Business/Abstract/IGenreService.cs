using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IGenreService
    {
         Task AddGenre(CreateGenreDto dto);
        Task<List<Genre>> GetGenres();
    }
}
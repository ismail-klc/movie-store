using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IDirectorService
    {
        Task AddDirector(CreateDirectorDto directorDto);
        Task<List<Director>> GetDirectors();
    }
}
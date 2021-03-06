using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;
using Entities.ViewModels;

namespace Business.Abstract
{
    public interface IDirectorService
    {
        Task AddDirector(CreateDirectorDto directorDto);
        Task UpdateDirector(UpdateDirectorDto dto);
        Task<List<DirectorViewModel>> GetDirectors();
        Task<DirectorViewModel> GetDirectorById(int id);
    }
}
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAdminService
    {
        Task<Admin> Create(CreateAdminDto dto);
        Task<string> Login(LoginDto dto);
        Task<Admin> GetByEmail(string email);
        Task<Admin> GetById(int id);
    }
}
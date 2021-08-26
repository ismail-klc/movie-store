using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Task<Customer> Create(CreateCustomerDto dto);
        Task<string> Login(LoginDto dto);
        Task<Customer> GetByEmail(string email);

        Task DeleteCustomerById(int id);

    }
}
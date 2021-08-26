using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Exceptions;
using Business.Helpers.Jwt;
using Data.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;



        public CustomerService(MovieContext context, IMapper mapper, IJwtService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        public async Task<Customer> Create(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);

            var addedCustomer = _context.Entry(customer);
            addedCustomer.State = EntityState.Added;

            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var customer = await this.GetByEmail(dto.Email);
            if (customer == null)
            {
                throw new BadRequestException("This email is not registered");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, customer.Password))
            {
                throw new BadRequestException("Invalid credentials");
            }

            var jwt = _jwtService.Generate(customer.Id, "customer");
            return jwt;
        }

        public async Task<Customer> GetByEmail(string email)
        {
            var customer = await _context.Set<Customer>().SingleOrDefaultAsync(x => x.Email == email);

            return customer;
        }

        public Task DeleteCustomerById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
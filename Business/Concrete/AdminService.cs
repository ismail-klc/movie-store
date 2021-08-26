using System.Threading.Tasks;
using auth.Helpers;
using AutoMapper;
using Business.Abstract;
using Business.Exceptions;
using Data.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class AdminService : IAdminService
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;



        public AdminService(MovieContext context, IMapper mapper, JwtService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<Admin> Create(CreateAdminDto dto)
        {
            var admin = _mapper.Map<Admin>(dto);
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);

            var addedAdmin = _context.Entry(admin);
            addedAdmin.State = EntityState.Added;

            await _context.SaveChangesAsync();

            return admin;
        }

        public async Task<Admin> GetByEmail(string email)
        {
            var admin = await _context.Set<Admin>().SingleOrDefaultAsync(x => x.Email == email);

            return admin;
        }

        public async Task<Admin> GetById(int id)
        {
            var admin = await _context.Set<Admin>().SingleOrDefaultAsync(x => x.Id == id);

            return admin;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var admin = await this.GetByEmail(dto.Email);
            if (admin == null)
            {
                throw new BadRequestException("This email is not registered");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, admin.Password))
            {
                throw new BadRequestException("Invalid credentials");
            }

            var jwt = _jwtService.Generate(admin.Id, "admin");
            return jwt;
        }
    }
}